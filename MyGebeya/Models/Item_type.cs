//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyGebeya.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item_type()
        {
            this.Items_for_rent = new HashSet<Items_for_rent>();
            this.Items_for_sale = new HashSet<Items_for_sale>();
        }
    
        public int item_type_id { get; set; }
        public string item_type_name { get; set; }
        public string item_type_description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Items_for_rent> Items_for_rent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Items_for_sale> Items_for_sale { get; set; }
    }
}
