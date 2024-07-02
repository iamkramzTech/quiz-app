using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebQuizApp.Services;

namespace WebQuizApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

            // Add services to the container.
            services.AddMvc();

            //Configure the distributed memory cache
            services.AddDistributedMemoryCache();

            // Add session services
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); //adjust timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add TempData providers
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddHttpClient<TriviaService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseStaticFiles() should be placed before app.UseRouting().
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // UseSession should be placed before any middleware that might use session state
            app.UseSession();

            // If it has any authorization middleware, it should go here.
            // app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Quiz}/{action=StartQuiz}/{id?}");

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TriviaQuiz}/{action=QuizSettings}/{id?}");
            });
            
        }
    }
}
