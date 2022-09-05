//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobHunt.Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Experience
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Experience()
        {
            this.Candidates = new HashSet<Candidate>();
            this.RecruitJobs = new HashSet<RecruitJob>();
        }
    
        public int ExperienceId { get; set; }
        public Nullable<double> EMin { get; set; }
        public Nullable<double> EMax { get; set; }
        public string EShow { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidate> Candidates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecruitJob> RecruitJobs { get; set; }
    }
}
