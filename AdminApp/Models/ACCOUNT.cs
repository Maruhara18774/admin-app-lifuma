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
    
    public partial class ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT()
        {
            this.COMMENTs = new HashSet<COMMENT>();
            this.CUSTOMERs = new HashSet<CUSTOMER>();
            this.FAVORITEs = new HashSet<FAVORITE>();
            this.RATINGs = new HashSet<RATING>();
        }
    
        public int ACCOUNT_ID { get; set; }
        public string ACCOUNT_FIRSTNAME { get; set; }
        public string ACCOUNT_LASTNAME { get; set; }
        public string EMAIL { get; set; }
        public string ACCOUNT_PASSWORD { get; set; }
        public string PHONE { get; set; }
        public string ACCOUNT_ADDRESS { get; set; }
        public Nullable<int> ROLE_ID { get; set; }
        public Nullable<int> MEMBER_ID { get; set; }
    
        public virtual MEMBER MEMBER { get; set; }
        public virtual ROLE ROLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER> CUSTOMERs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAVORITE> FAVORITEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATING> RATINGs { get; set; }

        public List<ROLE> lsROLES { get; set; }
        public List<MEMBER> lsMEMBERS { get; set; }
    }
}
