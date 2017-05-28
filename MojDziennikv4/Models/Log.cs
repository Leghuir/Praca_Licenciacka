﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojDziennikv4.Models
{
    public class Log
    {
        public String oeration { get; set; }
        public DateTime operationTime { get; set; }
        public int personId { get; set; }
        public String record { get; set; }
        public Log(String operation,DateTime operationTime,int personId,String record)
        {
            this.oeration = operation;
            this.operationTime = operationTime;
            this.personId = personId;
            this.record = record;
        }
    }
}