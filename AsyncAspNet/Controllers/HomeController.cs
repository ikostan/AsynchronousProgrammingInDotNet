using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncAspNet.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var httpMessage = await client.GetAsync("http://www.filipekberg.se/rss/").ConfigureAwait(false);

                var content = await httpMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                return Content(content);
            }
        }
    }
}