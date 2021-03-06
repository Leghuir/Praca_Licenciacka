﻿using MojDziennikv4.Models;
using MojDziennikv4.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojDziennikv4.Controllers
{
    public class ParticalLessonController : Controller
    {
        private MojDziennikEntities db = new MojDziennikEntities();
        public static String currentLessonName="";
        // GET: ParticalLesson
        public ActionResult LessonInDay()
        {
            String classNumer = PersonAccount.GetPupilFromAccountId().Class_Number;
            db.Lesson.Where(a => a.Class_Number.Equals(classNumer) && a.Day_Of_Week.Equals(3)).ToList(); //potem dowalić jeszcze w tym dniu
            return PartialView(db.Lesson.Where(a => a.Class_Number.Equals(classNumer) && a.Day_Of_Week.Equals(3)).ToList());
        }
        public ActionResult TeacherLessonInDay()
        {
            Employee teacher=PersonAccount.GetEmployeeFromAccountId();
            List<Lesson> lessons = teacher.Lesson.ToList();
            return PartialView(lessons);
        }
        public ActionResult CurrentLesson()
        {
            string now = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            String classNumer = PersonAccount.GetPupilFromAccountId().Class_Number;
            List<Lesson> lessons=db.Lesson.Where(a => a.Class_Number.Equals(classNumer) && a.Day_Of_Week.Equals(3)).ToList();
             
              var def=  lessons.Where(a => this.compareTimeStings(now,a)).ToList().ElementAtOrDefault(0);
            if (def != null)
                ParticalLessonController.currentLessonName = def.Subject.Subject_Name;
            return PartialView(lessons.Where(a => this.compareTimeStings(now,a)).ToList());
        }
        public ActionResult TeacherNextLesson()
        {
            String time = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            int employeeId = PersonAccount.GetEmployeeFromAccountId().Employee_Id;
            List<Lesson> lessons = db.Lesson.Where(a => a.Employee_Id.Equals(employeeId) && a.Day_Of_Week.Equals(3)).ToList();
            Lesson ls = lessons.Where(a => this.compareTimeStings(addMinutesToTime(time, 55),a)).ToList().ElementAtOrDefault(0);
            if (ls != null)
                return PartialView(ls);
            int counter = 0;
            while (ls == null)
            {
                ls = lessons.Where(a => this.compareTimeStings(time,a)).ToList().ElementAtOrDefault(0);
                time = addMinutesToTime(time, 55);
                counter++;
                if (counter > 100)
                    return PartialView(null);
            }
            return PartialView(ls);
        }
        public ActionResult NextLesson()
        {
            String time = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            String classNumer = PersonAccount.GetPupilFromAccountId().Class_Number;
            List<Lesson> lessons = db.Lesson.Where(a => a.Class_Number.Equals(classNumer) && a.Day_Of_Week.Equals(3)).ToList();
            Lesson ls = lessons.Where(a => this.compareTimeStings(addMinutesToTime(time, 55),a)).ToList().ElementAtOrDefault(0);
            if(ls!=null)
            return PartialView(ls);
            int counter = 0;
            while (ls==null)
            {
                ls= lessons.Where(a => this.compareTimeStings(time, a)).ToList().ElementAtOrDefault(0);
                time = addMinutesToTime(time, 45);
                counter++;
                if (counter > 100)
                    return PartialView(null);
            }
            return PartialView(ls);
        }

        private bool compareTimeStings(String Start_time,Lesson lesson)
        {
            String[] time = Start_time.Split(':');
            int start_hour = int.Parse(time[0]);
            int start_minutes = int.Parse(time[1]);
            time = lesson.Start_Time.Split(':');
            int lessonHour=int.Parse(time[0]);
            int lessonMintues= int.Parse(time[1]);
            int[] nextLessonTime = addMinutesToTime(lessonHour, lessonMintues, 45);
            if ((start_hour > lessonHour) | (start_hour == lessonHour & start_minutes > lessonMintues))//czy lekcja juz wystartowala
            {
                if ((nextLessonTime[0] == start_hour & nextLessonTime[1] > start_minutes) | (start_hour<nextLessonTime[0]))
                    return true;
            }
            return false;
        }
        private int[] addMinutesToTime(int start_hour, int start_minutes, int add_minutes)
        {
            start_minutes += add_minutes;
            if (start_minutes >= 60)
            {
                start_minutes -= 60;
                start_hour += 1;
            }
            return new int[] { start_hour, start_minutes };
        }
        private string addMinutesToTime(string start_time, int add_minutes)
        {
            string[] temp = start_time.Split(':');
            int start_hour = int.Parse(temp[0]);
            int start_minutes = int.Parse(temp[1]);
            start_minutes += add_minutes;
            if (start_minutes >= 60)
            {
                start_minutes -= 60;
                start_hour += 1;
                if (start_hour == 24)
                    start_hour -= 24;
            }
            return start_hour.ToString() + ':' + start_minutes.ToString();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}