//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class COMMENT
    {
        public int COMMENT_ID { get; set; }
        public string CONTENT { get; set; }
        public Nullable<int> PRODUCT_ID { get; set; }
        public Nullable<int> ACCOUNT_ID { get; set; }
    
        public virtual ACCOUNT ACCOUNT { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}