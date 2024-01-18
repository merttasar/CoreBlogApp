using CoreBlogApp;
using CoreBlogApp.Data;
using CoreBlogApp.Data.Abstract;
using CoreBlogApp.Data.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Users/Login";
});

var app = builder.Build();

SeedData.TestVerileriniDoldur(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "post_details",
    pattern: "posts/details/{url}",
    defaults: new { Controller = "Posts", Action = "Details" });

app.MapControllerRoute(
    name: "post_by_tag",
    pattern: "posts/tag/{url}",
    defaults: new { Controller = "Posts", Action = "Index" });
app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new { Controller = "Posts", Action = "Profile" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}");

app.Run();
