using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>                          // ��
                                                                    // ��� �� ����������� �����. ��� ��������� � ������ ��� ��� ����� ��������� � �����
            {
                options.IdleTimeout= TimeSpan.FromMinutes(20);      // ��������� ������� � ��� ����  ����� �������� � ������� TimeSpan.  ��� ����� ����� ������
                options.Cookie.HttpOnly = true;                     // ��� ��������� ��������� Cokie - HttpOnly �������� ��� � ��� ������ � ����� ����� ����� �� ��������� java ��������
                                                                    // �.�. ������ ������ ����� ���������� ����
                options.Cookie.IsEssential= true;                   //
            });

            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<BookService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
