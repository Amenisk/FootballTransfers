//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FootballTransfers.ADO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Footballers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Footballers()
        {
            this.TransfersHistory = new HashSet<TransfersHistory>();
        }
    
        public int Footballer_Id { get; set; }
        public string FullName { get; set; }
        public int TransferCost { get; set; }
        public int Characteristic_Id { get; set; }
        public int FootballClub_Id { get; set; }
    
        public virtual Characterics Characterics { get; set; }
        public virtual FootballClub FootballClub { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransfersHistory> TransfersHistory { get; set; }
    }
}
