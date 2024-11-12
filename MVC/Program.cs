using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session sẽ tồn tại
	options.Cookie.HttpOnly = true; // Bảo mật session
	options.Cookie.IsEssential = true; // Bắt buộc phải có cookie
});
void ConfigureHttpClient(HttpClient client)
{
    client.BaseAddress = new Uri("https://localhost:7296/odata/");
}
builder.Services.AddHttpClient<ICustomerService, CustomerService>(ConfigureHttpClient);
builder.Services.AddHttpClient<ICarService, CarService>(ConfigureHttpClient);

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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
