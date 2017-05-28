using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MojDziennikv4.Models.DAL
{
    
    public class PersonAccount : IIdentity, IPrincipal
    {
        private static PersonAccount instance;
        private String login;
        private String type;
        public int accountId { get; set; }
        public bool legalGuardianLog { get; set; }
        private bool correct =false;
        private PersonAccount(String login,String type)
        {
            legalGuardianLog = false;
            this.login = login;
            this.type = type;
        }
        private PersonAccount()
        {
            legalGuardianLog = false;
            accountId = 0;
        }
        public void reset()
        {
            legalGuardianLog = false;
            login = "";
            type = "";
            accountId = 0;
            correct = false;
            IsAuthenticated = false;

        }
        public string Name
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }

        public string AuthenticationType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return correct;
            }
            set
            {
                correct = value;
            }
        }

        public IIdentity Identity
        {
           get { return PersonAccount.getInstance(); }
        }

        public static PersonAccount getInstance()
        {
            if(PersonAccount.instance!=null)
            {
                return PersonAccount.instance;
            }
            PersonAccount.instance = new PersonAccount();
            return PersonAccount.instance;
        }
        public static PersonAccount getInstance(String login,String type)
        {
            if (PersonAccount.instance != null)
            {
                return PersonAccount.instance;
            }
            PersonAccount.instance = new PersonAccount(login,type);
            return PersonAccount.instance;
        }
        public static Pupil GetPupilFromAccountId()
        {
            MojDziennikEntities db = new MojDziennikEntities();
            int accid = PersonAccount.getInstance().accountId;
            Account account = db.Account.Where(a => a.Account_Id == accid).FirstOrDefault();
            if (account == null)
                return new Pupil();
            return account.Pupil.ElementAt(0);
        }
        public static Legal_Guardian GetLegal_GuardianFromAccountId()
        {
            MojDziennikEntities db = new MojDziennikEntities();
            int accid = PersonAccount.getInstance().accountId;
            Account account = db.Account.Where(a => a.Account_Id == accid).FirstOrDefault();
            if (account == null)
                return new Legal_Guardian();
            return account.Legal_Guardian.ElementAt(0);
        }
        public static Employee GetEmployeeFromAccountId()
        {
            MojDziennikEntities db = new MojDziennikEntities();
            int accountid =PersonAccount.getInstance().accountId;
            var account = db.Account.Where(a => a.Account_Id == accountid).FirstOrDefault();
            //var temp = account
            return account.Employee.ElementAt(0);
        }
        public static int checkpasses(IEnumerable<Account> model, String login, String password, String at)
        {
            foreach (var m in model)
            {
                if (m.Login.Equals(login) && m.Password.Equals(password) && m.Account_Type.ToString().Equals(at))
                {
                    using (MojDziennikEntities db = new MojDziennikEntities())
                    { 
                        PersonAccount.instance.Name = login;
                    PersonAccount.instance.AuthenticationType = at;
                    PersonAccount.instance.accountId = m.Account_Id;
                    PersonAccount.instance.IsAuthenticated = true;
                    if (at=="Opiekun")
                    {
                            PersonAccount.instance.legalGuardianLog = true;
                            PersonAccount.instance.AuthenticationType = "Uczen";
                            PersonAccount.instance.accountId = db.Legal_Guardian.Where(a => a.Account_Id == PersonAccount.instance.accountId).ToList().FirstOrDefault().Pupil.ToList().FirstOrDefault().Account_Id;
                    }
                        switch (m.Account_Type.ToString())

                    {
                        case "Nauczyciel": return 1;
                        case "Uczen":
                        case "Opiekun": return 2;
                        default: return 3;
                    }
                    }
                }

            }
            return 0;
        }

        public bool IsInRole(string role)
        {
            return PersonAccount.getInstance().AuthenticationType.Equals(role);
        }
    }
}