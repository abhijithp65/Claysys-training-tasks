using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserRepository userRepository;

        public RegistrationController()
        {
           
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            userRepository = new UserRepository(connectionString);
        }

        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model, string action)
        {
            if (ModelState.IsValid)
            {
                switch (action)
                {
                    case "Insert":
                        userRepository.CreateUser(model);
                        ViewBag.Message = "Registration Successful!";
                        break;
                    case "View":
                        UserModel user = userRepository.GetUserByUsername(model.Username);
                        if (user != null)
                        {
                            return View("ViewUser", user);
                        }
                        else
                        {
                            ViewBag.Message = "User not found.";
                            break;
                        }

                    case "Delete":
                        userRepository.DeleteUserByUsername(model.Username);
                        ViewBag.Message = "User Deleted Successfully!";
                        break;
                    case "Update":
                        userRepository.UpdateUser(model);
                        ViewBag.Message = "User Information Updated!";
                        break;
                    default:
                        ViewBag.Message = "Invalid action.";
                        break;
                }
            }

            return View("Index", model);
        }
    }
}
