using BusinessLayer.Abstract;
using BusinessLayer.Concretae;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using DataAccessLayer.EntityFaremwork;
using EntityLayer.Concreate;
using IBlog.Hubs;
using IBlog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireNonAlphanumeric = true;
    x.Password.RequireUppercase = true;
    x.Password.RequireLowercase = true;
    x.Password.RequireDigit = true;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = false;
    x.SignIn.RequireConfirmedEmail = true;


}).AddEntityFrameworkStores<Context>().AddErrorDescriber<IdentityValidator>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(30);
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();
    config.Filters.Add(new AuthorizeFilter(policy));

}).AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(
               CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
               x =>
               {
                   x.LoginPath = "/Login/index/";
               });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.AccessDeniedPath = new PathString("/Login/AccessDenied");
    options.LoginPath = "/Login/Index/";
    options.LogoutPath = "/Login/SignOut/";
    options.SlidingExpiration = true;
}
);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
}
);




builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Index");
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areaRoute",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Blog}/{action=Index}/{id?}");
    
});
app.MapHub<ChatHub>("/ChatHub");

app.Run();
