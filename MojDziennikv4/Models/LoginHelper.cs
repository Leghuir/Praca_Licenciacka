using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojDziennikv4.Models
{
    public class LoginHelper :IEnumerable
    {
        public List<Account> db
        {
            get; set;
        }
        public String Password { get; set; }
        public String Login { get; set; }
        public String Type { get; set; }
        public LoginHelper(List<Account> listdb,String password,String login,String type)
        {
            if (password == null) password = "";
            if (login == null) login = "";
            if (type == null) type = "Unknow";
            this.db = listdb;
            this.Password = password;
            this.Login = login;
            this.Type = type;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}