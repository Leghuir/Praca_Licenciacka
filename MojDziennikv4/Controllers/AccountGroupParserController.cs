using MojDziennikv4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojDziennikv4.Controllers
{
    public class AccountGroupParserController : Controller
    {
        public String accountGroup { get; set; }
        private List<String> accountTypes;
        private List<String> aviblesclasses;
        private MojDziennikEntities db = new MojDziennikEntities();

        public AccountGroupParserController()
        {
            this.accountTypes = new List<string>();
            this.aviblesclasses = new List<string>();

        }
        public void init()
        {
            String[] mysplit = accountGroup.Split(':');
            fillAccountTypes(mysplit[0]);
            FillAvibleClasses(mysplit[1]);
        }
        private void FillAvibleClasses(String classes)
        {
            if (classes.Equals("all"))
            {
                foreach (var cl in db.School_Class.ToList())
                {
                    this.aviblesclasses.Add(cl.Class_Number);
                }
                aviblesclasses.Add("");
            }
            else
            {
                foreach (var cl in classes.Split(','))
                {
                    this.aviblesclasses.Add(cl);
                }
            }
        }
        private void fillAccountTypes(String types)
        {
            if (types.Equals("all"))
            {
                foreach (var acc in Enum.GetValues(typeof(AccountType)))
                {
                    accountTypes.Add(acc.ToString());
                }
                accountTypes.Add("");
            }
            else
            {
                foreach (var ty in types.Split(','))
                {
                    this.aviblesclasses.Add(ty);
                }
            }
        }
        public bool exits(String type, String userclass)
        {
            if (this.accountTypes.Contains(type) && this.aviblesclasses.Contains(userclass))
                return true;
                return false;
        }
    }
}