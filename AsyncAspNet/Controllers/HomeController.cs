using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncAspNet.Controllers
{
    public class HomeController : Controller
    {
        /*
        //Simple method
        public ActionResult Index()
        {
            return Content("Hello World!");
        }
        */
      
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var httpMessage = await client.GetAsync("http://www.filipekberg.se/rss/"); //.ConfigureAwait(false);
                //var httpMessage = await client.GetAsync("http://www.cic.gc.ca/english/helpcentre/answer.asp?qnum=1181&top=15").ConfigureAwait(false);

                var content = await httpMessage.Content.ReadAsStringAsync(); //.ConfigureAwait(false);

                return Content(content);
            }
        }      
    }
}