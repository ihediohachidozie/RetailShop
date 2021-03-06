//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetailShop
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalesOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesOrder()
        {
            this.Customers = new HashSet<Customer>();
            this.SoldItems = new HashSet<SoldItem>();
            this.SalesReturns = new HashSet<SalesReturn>();
        }
    
        public int Id { get; set; }
        public string Salesno { get; set; }
        public int PaymentMode { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Amt_Tendered { get; set; }
        public int Createdby { get; set; }
        public System.DateTime Createdon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoldItem> SoldItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesReturn> SalesReturns { get; set; }
    }
}
