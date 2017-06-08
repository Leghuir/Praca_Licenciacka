using MojDziennikv4.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MojDziennikv4.Models
{
    public static class LogManager
    {
        private static String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory+@"log.txt");
        public static Log[] GetAllLogs()
        {
            List<Log> listOfLogs = new List<Log>();
            using (StreamReader sr = new StreamReader(path))
            {

                String text = sr.ReadToEnd();
                String[] logs = text.Split(';');
                foreach(string log in logs)
                {
                    string[] temp = log.Split(',');
                    if(temp.Length==4)
                    listOfLogs.Add(new Log(temp[0], new DateTime(long.Parse(temp[1])), int.Parse(temp[2]),temp[3]));
                }
                return listOfLogs.ToArray();
            }
        }
        public static void SaveLog(String log)
        {
           // File.Create(path);
            using (StreamWriter sw = new StreamWriter(path,append:true))
            {
                
                sw.Write(log+';');
            }
        }
        public static void createlog(String operation,String item)
        {
            LogManager.SaveLog(operation + ',' + DateTime.Now.Ticks + ',' + PersonAccount.getInstance().accountId + ',' + item);
        }
        public static void createeditlog(String item)
        {
            LogManager.SaveLog("Edit" + ',' + DateTime.Now.Ticks + ',' + PersonAccount.getInstance().accountId + ',' + item);
        }
        public static void createdeletelog(String item)
        {
            LogManager.SaveLog("delete" + ',' + DateTime.Now.Ticks + ',' + PersonAccount.getInstance().accountId + ',' + item);
        }
    }
}