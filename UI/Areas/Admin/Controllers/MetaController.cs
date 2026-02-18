using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class MetaController : Controller
    {
        MetaBLL metaBLL = new MetaBLL();
        // GET: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddMeta(MetaDTO model)
        {
            if(ModelState.IsValid)
            {
                if (metaBLL.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                }else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
            }else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }

        public ActionResult MetaList()
        {
            List<MetaDTO> metaDtoList = metaBLL.GetMetaData();
            return View(metaDtoList);
        }

        public ActionResult UpdateMeta(int ID)
        {
            MetaDTO model = new MetaDTO();
            model = metaBLL.GetMetaWithID(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateMeta(MetaDTO model)
        {
            if(ModelState.IsValid)
            {
                if(metaBLL.UpdateMeta(model))
                {
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState = General.Message.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }
    }
}