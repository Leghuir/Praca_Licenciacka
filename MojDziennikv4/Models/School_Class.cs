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
    
    public partial class School_Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public School_Class()
        {
            this.Lesson = new HashSet<Lesson>();
            this.Pupil = new HashSet<Pupil>();
        }
    
        public string Class_Number { get; set; }
        public string Profile { get; set; }
        public Nullable<int> Employee_Id { get; set; }

        public override string ToString()
        {
            return "School Class Class number: " + Class_Number + " Profile: " +Profile + " Employee Id: " + Employee_Id;
        }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lesson> Lesson { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pupil> Pupil { get; set; }
    }
}
