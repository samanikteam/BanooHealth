using Data;
using Data.Contracts;
using Data.Repositories;
using Entities.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web
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
            //https part1
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            //endhttps
            services.AddRazorPages();
            //services.AddDbContext(Configuration);

            #region Slug maker
            services.AddRouting(options=>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
            #endregion

            #region Identity


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region Database

            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            #endregion

            #region IOC
            services.AddTransient<IArticleRepasitory, ArticleRepasitory>();
            services.AddTransient<IArticelCategoryRepasitory, ArticleCategoryRepasitory>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductArticleRepository, ProductArticleRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<IBannerRepository, BannerRepository>();
            services.AddTransient<IProGalleryRepository, ProGalleryRepository>();
            services.AddTransient<ISearchLiveRepository, SearchLiveRepository>();
            services.AddTransient<ISiteSettingRepository, SiteSettingRepository>();
            services.AddTransient<IPharmacyRepository, PharmacyRepository>();
            services.AddTransient<ISloganRepository, SloganRepository>();
            services.AddTransient<ILoginSlider, LoginSliderRepository>();
            services.AddTransient<IProCommentRepository, ProCommentRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IUserRepasitory, UserRepasitory>();
            services.AddTransient<IPharmacyProduct, PharmacyProductsRepository>();
            services.AddTransient<IFilterRepository, FilterRepository>();
            services.AddTransient<IProductFilterRepository, ProductFilterRepository>();
            services.AddTransient<IProCategoriesRepository, ProCategoriesRepository>();
            services.AddTransient<IArticleCategoryAssignRepository, ArticleCategoryAssignRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IContactUsRepository, ContactUsRepository>();
            services.AddTransient<ICallRepository, CallRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {



            //https part2
            var options = new RewriteOptions().AddRedirectToHttps(StatusCodes.Status301MovedPermanently, 63423);
            app.UseRewriter(options);
            //app.UseMvc();
            //end https

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
