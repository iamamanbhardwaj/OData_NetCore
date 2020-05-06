using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using System.Linq;
using WebAPIsWithOData.Models;

namespace WebAPIsWithOData
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
            //register the database context

            //services.AddDbContext<BookStoreContext>(opt => opt.UseInMemoryDatabase("BookLists")); // for this demo , using in memory database

            services.AddDbContext<BookStoreContext>(options => {
                //options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Register the OData Services through Dependency Injection
            services.AddOData(); // this is an extension method under Microsoft.AspNet.OData.Extensions

            services.AddMvc(options =>
            {
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("xml", "application/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("config", "application/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat
                    ("js", "application/json");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            //http://localhost:5000/odata/books?format=xml

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvc();  //by default using Mvc in pipeline

            //add OData route to register the OData endpoint.

            app.UseMvcWithDefaultRoute();

            app.UseMvc(rb =>
            {
                // register query parameters to support such as select, expand, filter, etc
                rb.Select().Expand().OrderBy().MaxTop(100).Count().SkipToken().Filter();

                rb.MapODataServiceRoute("odata", "odata", GetEdmModel());


            });

        }

        /// <summary>
        /// This method builds Entity Data Model (EDM) to describe the structure of data.
        /// Defined two entity set named "Books" and "Presses".
        /// </summary>
        /// <returns></returns>
        private static IEdmModel GetEdmModel()
        { 
            var builder = new ODataConventionModelBuilder();

            // telling OData model builder about entity sets to register as a part of EDM 
            builder.EntitySet<Book>("Books");

            var book =  builder.EntityType<Book>();

            // remove email from Presses type in edm model 
            builder.EntitySet<Press>("Presses").EntityType.Ignore(s => s.Email);

            builder.EntitySet<Press>("Presses");

            book.Function("GetPress").Returns<Press>();


            builder.ContainerName = "Container";

            return builder.GetEdmModel();
        }
    }
}
