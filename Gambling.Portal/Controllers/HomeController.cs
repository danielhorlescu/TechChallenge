using System.Web.Mvc;
using Gambling.Persistance;

namespace Gambling.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public HomeController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}