namespace ASP.NET_CORE
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm cấu hình Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();

            // Cấu hình các dịch vụ cần thiết khác
            // ...
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ...

            // Sử dụng Session Middleware
            app.UseSession();

            // ...
        }


    }
}