using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SlalomTricks.Models;
using Microsoft.AspNetCore.Http;

namespace SlalomTricks.Controllers
{
    public class TrickController : Controller
    {
         
        // Função GET de Trick
        public ActionResult Index()
        {
            using (TricksModel model = new TricksModel())
            {
                List<Trick> lista = model.Read();
                return View(lista);
            }
                
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IFormCollection form)
        {
            Trick trick = new Trick
            {
                Name = form["Name"],
                Family = form["Family"],
            };

            int parsedInt = 0;
            if (int.TryParse(form["Value"], out parsedInt))
            {
                trick.Value = parsedInt;
            }
            else
            {

            }

            using (TricksModel model = new TricksModel())
            {
                model.Create(trick);
                return RedirectToAction("Index");

            }
        }

    }
}