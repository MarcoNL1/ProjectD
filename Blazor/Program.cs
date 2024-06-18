using Blazor.Components;
using Blazor.Components.Account;
using Common.Models;
using Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    // Microsoft Authentication (not complete)
    // .AddMicrosoftAccount(microsoftOptions =>
    // {
    //     microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
    //     microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
    // })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("Database") ??
                       throw new InvalidOperationException("Connection string 'Database' not found.");
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddRoleStore<RoleStore<IdentityRole, AppDbContext>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

builder.Services.AddAuthorizationBuilder()
    .AddFallbackPolicy("RequireAuthenticatedUser", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser().Build();
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AllowAnonymousStaticFiles", policyBuilder =>
    {
        policyBuilder.RequireAssertion(context =>
        {
            var path = (context.Resource as HttpContext)?.Request.Path;
            Console.WriteLine("Resource: " + context.Resource + "\nPATH: " + (path.HasValue ? path.Value : path.HasValue));
            return path.HasValue && (path.Value.ToString().EndsWith("css") ||
                                     path.Value.ToString().EndsWith(".js") ||
                                     path.Value.ToString().EndsWith(".png"));
        });
    });

builder.Services.AddTransient<RoomService>();
builder.Services.AddTransient<ReservationService>();
builder.Services.AddTransient<WorkspaceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// User & Role seeding
using (var scope = app.Services.CreateScope())
{
    const string email = "admin@admin.com";
    const string password = "Admin1!";
    string[] roles = ["Admin", "Manager", "User"];
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            var roleRole = new IdentityRole(role);
            await roleManager.CreateAsync(roleRole);
        }
    }
    if (await userManager.FindByNameAsync(email) == null)
    {
        var user = new User
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Use(async (context, next) =>
{
    var path = context.Request.Path;
    if (path.HasValue && (path.Value.EndsWith(".css") || path.Value.EndsWith(".js") || path.Value.EndsWith(".png")))
    {
        var authService = context.RequestServices.GetRequiredService<IAuthorizationService>();
        var authResult = await authService.AuthorizeAsync(context.User, context, "AllowAnonymousStaticFiles");

        if (authResult.Succeeded)
        {
            await next();
            return;
        }
    }

    await next();
});

app.Run();


