using MojDziennikv4.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojDziennikv4.Models
{
    public class Person
    {
        public static IPerson GetPerson()
        {
            MojDziennikEntities db = new MojDziennikEntities();
            switch (PersonAccount.getInstance().AuthenticationType)
            {
                case "Nauczyciel": return PersonAccount.GetEmployeeFromAccountId();
                case "Uczen": return PersonAccount.GetPupilFromAccountId();
                case "Opiekun": return PersonAccount.GetLegal_GuardianFromAccountId();
                default: return PersonAccount.GetEmployeeFromAccountId();
            }
        }
    }
}