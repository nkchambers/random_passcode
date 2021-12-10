using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // This is where session comes from


namespace random_passcode.Controllers
{
    public class HomeController : Controller
    {
        private string generatePasscode(int charNum)
        {
            Random randChar = new Random();
            string charValues = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string result = "";

            for(var i=0; i<charNum; i++)
            {
                result += charValues[randChar.Next(charValues.Length)];
            
            }
            
            return result;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("passcode") == null)
                {
                    HttpContext.Session.SetString("passcode", "Generate a Passcode!");
                }
                
            if(HttpContext.Session.GetInt32("count") == null)
                {
                    HttpContext.Session.SetInt32("count", 0);
                }
            

            ViewBag.Passcode = HttpContext.Session.GetString("passcode");
            ViewBag.Count = HttpContext.Session.GetInt32("count");

            return View();
        }


        [HttpGet("new_passcode")]
        public IActionResult NewPasscode()
        {
            int? count = HttpContext.Session.GetInt32("count");
            count++;
            HttpContext.Session.SetInt32("count", (int)count);
            HttpContext.Session.SetString("passcode", generatePasscode(14));

            return RedirectToAction("Index");
        }
    }
}