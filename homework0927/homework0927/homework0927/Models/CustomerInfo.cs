//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace homework0927.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerInfo()
        {
            this.OrderInfoes = new HashSet<OrderInfo>();
        }
    
        public int CustId { get; set; }
        public string CustName { get; set; }
        public string CustRoad { get; set; }
        public string CustCellPhone { get; set; }
        public Nullable<int> PostId { get; set; }
        public int PostCategory { get; set; }
        public string CustLoginName { get; set; }
        public string CustPassword { get; set; }
    
        public virtual CustLogin CustLogin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfoes { get; set; }
    }
}
