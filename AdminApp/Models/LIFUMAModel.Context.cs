﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LIMUPAStoreEntities : DbContext
    {
        public LIMUPAStoreEntities()
            : base("name=LIMUPAStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<CART> CARTs { get; set; }
        public virtual DbSet<CARTDETAIL> CARTDETAILs { get; set; }
        public virtual DbSet<COMMENT> COMMENTs { get; set; }
        public virtual DbSet<FAVORITE> FAVORITEs { get; set; }
        public virtual DbSet<FEATURE> FEATUREs { get; set; }
        public virtual DbSet<MEMBER> MEMBERs { get; set; }
        public virtual DbSet<ORDER> ORDERS { get; set; }
        public virtual DbSet<ORDERSTATU> ORDERSTATUS { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PRODUCER> PRODUCERs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<PRODUCTTYPE> PRODUCTTYPEs { get; set; }
        public virtual DbSet<RATING> RATINGs { get; set; }
        public virtual DbSet<ROLE> ROLES { get; set; }
    }
}