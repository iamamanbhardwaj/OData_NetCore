using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIsWithOData.Models;

namespace WebAPIsWithOData.Controllers
{
    [Route("api/[controller]")]
    // ODataController inherits ControllerBase and defines a base class for OData controllers that support writing and reading data using the OData formats.
    public class BooksController : ODataController
    {
        private BookStoreContext _dbContext;

        /// <summary>
        /// Constructor adds Books and Presses Records into BookStoreContext from data source (if Books count is zero)
        /// </summary>
        /// <param name="context"></param>
        public BooksController(BookStoreContext context)
        {
            _dbContext = context;
        }

        // disables nested expansions
        //[EnableQuery(MaxExpansionDepth =1)]
        // EnableQuery : Enables a controller action to support OData query parameters/syntaxs 
        [EnableQuery(PageSize = 5, AllowedQueryOptions = (AllowedQueryOptions.Select))]
        //[EnableQuery]
        public IQueryable<Book> Get()
        {
            return _dbContext.Books;
        }

        //http://localhost:5000/odata/books(2)/getpress
        // must add function to book entity set in getedmmodel() in startup 
        [EnableQuery]
        public IQueryable GetPress(int key)
        {
            return _dbContext.Presses.Where(b => b.Id == key);
            
        }

        [HttpPost]
        public IActionResult Post(Book book)

        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return new CreatedResult("Books", book);
        }

        /// <summary>
        /// get individual resource
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<Book> Get([FromODataUri] int key)
        {
            return _dbContext.Books.Where(s=>s.Id == key);
        }

        [HttpGet]
        [Route("normal-api")]
        public IActionResult NormalAPI()
        {
            return Ok("you just hit an non-odata endpoint !!");
        }
    }
}