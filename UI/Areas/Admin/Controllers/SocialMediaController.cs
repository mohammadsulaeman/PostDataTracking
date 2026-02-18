using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using System.Drawing;
using System.IO;
namespace UI.Areas.Admin.Controllers
{
    public class SocialMediaController : Controller
    {
        SocialMediaBLL bll = new SocialMediaBLL();
        // GET: Admin/SocialMedia
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if(model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            }else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.SocialImage;
                Bitmap SocilaMedia = new Bitmap(postedFile.InputStream);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if(ext ==".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".png")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedFile.FileName;
                    SocilaMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                    model.ImagePath = filename;
                    if(bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Message.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
                    }else
                    {
                        ViewBag.ProcessState = General.Message.GeneralError;
                    }
                }else
                {
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }
            }else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }

        public ActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtoList = new List<SocialMediaDTO>();
            dtoList = bll.GetSocialMedia();
            return View(dtoList);
        }

        public ActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);

            return View(dto);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if(ModelState.IsValid)
            {
               if(model.SocialImage != null)
               {
                    HttpPostedFileBase postedFile = model.SocialImage;
                    Bitmap SocilaMedia = new Bitmap(postedFile.InputStream);
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".png")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedFile.FileName;
                        SocilaMedia.Save(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + filename));
                        model.ImagePath = filename;
                        
                    }


                }
                string oldImagePath = bll.UpdateSocialMedia(model);
                if(model.SocialImage != null)
                {
                    if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" +oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/SocialMediaImages/" + oldImagePath));
                    }
                }
                model = new SocialMediaDTO();
                ViewBag.ProcessState = General.Message.UpdateSuccess;
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }
    }
}