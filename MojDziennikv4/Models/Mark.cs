//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MojDziennikv4.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mark
    {
        public int Mark_Id { get; set; }
        public int Value { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public int Pupil_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Subject_Id { get; set; }
        public string Describe { get; set; }
        public System.DateTime Mark_Date { get; set; }

        public override string ToString()
        {
            return "Mark Id: " + Mark_Id + " Value: " +
                Value + " Weight: " + Weight.GetValueOrDefault() +
                " Pupil Id: " + Pupil_Id +  " Employee Id: " + Employee_Id +
                " Subject Id: " + Subject_Id + " Describe: " + Describe + " Mark date: "+Mark_Date.ToShortDateString();
        }

        public virtual Employee Employee { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Pupil Pupil { get; set; }
    }
}
