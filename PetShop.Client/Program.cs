using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Data.Repositories;
using PetShop.Model.Entities;
using PetShop.Services;

var builder = WebApplication.CreateBuilder(args);
var petShopConnection = builder.Configuration.GetConnectionString("PetShopConnection");
var accountConnection = builder.Configuration.GetConnectionString("PetShopAccountsConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PetShopDbContext>(options =>
		options.UseSqlServer(petShopConnection));
builder.Services.AddDbContext<PetShopAccountsDbContext>(options =>
		options.UseSqlServer(accountConnection));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<PetShopAccountsDbContext>();

builder.Services.AddTransient<IRepository<Animal>, AnimalRepository>();
builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<IPetShopService<Animal>, AnimalsService>();
builder.Services.AddTransient<IPetShopService <Category>, CategoryService>();
builder.Services.AddSingleton<ImageService>();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

var app = builder.Build();


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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
