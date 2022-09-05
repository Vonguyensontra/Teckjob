﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JobHuntEntities : DbContext
    {
        public JobHuntEntities()
            : base("name=JobHuntEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidatePostResume> CandidatePostResumes { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CompanySize> CompanySizes { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<LevelInfo> LevelInfoes { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Recruit> Recruits { get; set; }
        public virtual DbSet<RecruitJob> RecruitJobs { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<SaveCandidate> SaveCandidates { get; set; }
        public virtual DbSet<SaveJob> SaveJobs { get; set; }
        public virtual DbSet<SignUpNewsletter> SignUpNewsletters { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<WebmasterInfo> WebmasterInfoes { get; set; }
        public virtual DbSet<Website> Websites { get; set; }
        public virtual DbSet<WorkType> WorkTypes { get; set; }
    }
}
