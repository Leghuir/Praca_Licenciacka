using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MojDziennikv4.Models
{
    public class LogManager
    {
        private static String path = @"D:\log.txt";
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
                    if(temp.Length==3)
                    listOfLogs.Add(new Log(temp[0], new DateTime(long.Parse(temp[1])), int.Parse(temp[2])));
                }
                return listOfLogs.ToArray();
            }
        }
        public static void SaveLog(String log)
        {
           // File.Create(path);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(log+';');
            }
        }
    }
}