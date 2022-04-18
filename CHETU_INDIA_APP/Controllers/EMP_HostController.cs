using CHETU_INDIA_APP.DB_Context;
using CHETU_INDIA_APP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHETU_INDIA_APP.Controllers
{
    public class EMP_HostController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
      
            public IActionResult emp_list()
            {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            List<empmodel> mod = new List<empmodel>();
            var res = ent.ChetuEmplyoyees.ToList();
            foreach (var item in res)
            {
                mod.Add(new empmodel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    City = item.City,
                    Designation = item.Designation,
                    Mobile = item.Mobile

                });

            }


            return View(mod);
            }
        [HttpGet]
        public IActionResult add_employee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add_employee(empmodel mod )
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            ChetuEmplyoyee tab = new ChetuEmplyoyee();
            tab.Id = mod.Id;
            tab.Name = mod.Name;
            tab.Email = mod.Email;
            tab.City = mod.City;
            tab.Designation = mod.Designation;
            tab.Mobile = mod.Mobile;
            if (mod.Id == 0)
            {
                ent.ChetuEmplyoyees.Add(tab);
                ent.SaveChanges();
            }
            else
            {
                ent.Entry(tab).State = EntityState.Modified;
                ent.SaveChanges();
            }
            return RedirectToAction("emp_list","EMP_Host");
        }
        public IActionResult Edit(int id)
        {
            empmodel mod = new empmodel();
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            var edit = ent.ChetuEmplyoyees.Where(m => m.Id == id).First();
            mod.Id = edit.Id;
            mod.Name = edit.Name;
            mod.Email = edit.Email;
            mod.City = edit.City;
            mod.Designation = edit.Designation;
            mod.Mobile = edit.Mobile;

            return View("add_employee", mod);
        }
        public IActionResult Delete(int id)
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            var dlt = ent.ChetuEmplyoyees.Where(m => m.Id == id).First();
            ent.ChetuEmplyoyees.Remove(dlt);
            ent.SaveChanges();
            return RedirectToAction("emp_list"); 
        }
        public IActionResult userlist()
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            var dt = ent.LoginInformationTables.ToList();

            return View(dt);
        }

        [HttpGet]
        public IActionResult userragistrationaddmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult userragistrationaddmin(usermodel mod)
        {
            chetu_India_CompanyContext ent = new chetu_India_CompanyContext();
            LoginInformationTable logtb = new LoginInformationTable();
            logtb.Id = mod.Id;
            logtb.Name = mod.Name;
            logtb.Email = mod.Email;
            logtb.Password = mod.Password;
            ent.LoginInformationTables.Add(logtb);
            ent.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult indexdashboard1()
        {
            return View();
        }
        public IActionResult _Addnavbar()
        {
            return View();
        }


    }
}

