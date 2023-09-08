using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ImageController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Add(Image model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    string imagePath = Path.Combine(Server.MapPath("~/Images/"), filename);
                    model.ImageFile.SaveAs(imagePath);

                    using (DbModels db = new DbModels())
                    {
                        model.ImagePath = "~/Images/" + filename;
                        db.Images.Add(model);
                        db.SaveChanges();
                    }

                    // Redirect to the "Index" action after successful upload
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error uploading the image: " + ex.Message;
                }
            }

            return View(model);
        }




        public ActionResult Index()
        {
            using (DbModels db = new DbModels())
            {
                var images = db.Images.ToList();
                return View("view", images); 
            }
        }


        [HttpGet]
        public ActionResult View(int id)
        {
            using (DbModels db = new DbModels()) 
            {
                var model = db.Images.FirstOrDefault(x => x.ImageId == id);
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
    }
}
