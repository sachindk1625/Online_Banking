using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankWebApplication.Models;

namespace BankWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpGet]
        public IActionResult NewBankAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewBankAccount( AccountDetails account)
        {
           if(ModelState.IsValid)
            {
                BankDBContext db = new BankDBContext();
                int accNum = 0;
                var nextAccountNumber = (from num in db.NextAccountNumber
                                         select num).Last();
                accNum = 1 + nextAccountNumber.NextId;
                account.AccountNumber = accNum;
                db.AccountDetails.Add(account);
                db.SaveChanges();

                NextAccountNumber nextId = new NextAccountNumber();
                nextId.NextId = accNum;
                db.NextAccountNumber.Add(nextId);
                db.SaveChanges();
                ViewBag.data = accNum;
                return View("Confirmation");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult NewOnlineAccount()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult NewOnlineAccount(AccountDetails account)
        {
            BankDBContext db = new BankDBContext();
            List<AccountDetails> values = (from acc in db.AccountDetails
                                                 where acc.Ssn == account.Ssn
                                                 select acc).ToList();
            if(values.Count==0)
            {
                ViewBag.err = "You dont have an account in KALE'S bank";
                return View(account);
            }
            return View("SelectAccount",values);
        }

        [HttpPost]
        public IActionResult SelectAccount(string accNo)
        {
            int AccNo = Convert.ToInt32(accNo);
            BankDBContext db = new BankDBContext();

            var error = (from acc in db.OnlineAccounts
                         where acc.AccountNumber == AccNo
                         select acc).ToList();

            AccountDetails account = (from acc in db.AccountDetails
                                      where acc.AccountNumber == AccNo
                                      select acc).FirstOrDefault();
            ViewBag.data = account;
            if (error.Count!=0)
            {
                List<AccountDetails> values = (from acc in db.AccountDetails
                                               where acc.Ssn == account.Ssn
                                               select acc).ToList();
                ViewBag.err = "Online Account for account number " + AccNo + " already exists";
                return View(values);
            }

            
            return View("CreateOnlineAccount");
        }

        public IActionResult OnlineAccount(OnlineAccounts account)
        {
            BankDBContext db = new BankDBContext();
            var error = (from acc in db.OnlineAccounts
                         where acc.UserName == account.UserName
                         select acc).ToList();

            if(error.Count!=0)
            {
                AccountDetails Acc = (from acc in db.AccountDetails
                                          where acc.AccountNumber == account.AccountNumber
                                          select acc).FirstOrDefault();
                ViewBag.data = Acc;
                ViewBag.err = "The username already exists";
                return View("CreateOnlineAccount",account);
            }

            db.OnlineAccounts.Add(account);
            db.SaveChanges();
            return View("Welcome");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

       [HttpPost]
        public IActionResult SignIn(OnlineAccounts credentials)
        {
            BankDBContext db = new BankDBContext();
            var account = (from acc in db.OnlineAccounts
                          where acc.UserName == credentials.UserName && acc.Password == credentials.Password
                          select acc).ToList();

            if(account.Count==0)
            {
                ViewBag.err = "Either Username or Password is wrong";
                return View(credentials);
            }
            return View("welcome");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
