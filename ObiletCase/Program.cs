using ObiletCase.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IObiletApiService, ObiletApiService>(); // burada Obilet API servisini DI konteynerine ekliyoruz
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(); // Session yönetimi için gerekli servisleri ekliyoruz

var app = builder.Build();
app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
