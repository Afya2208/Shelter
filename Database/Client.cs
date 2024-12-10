//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shelter.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Donation = new HashSet<Donation>();
            this.TakingAnimal = new HashSet<TakingAnimal>();
        }
    
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalZip { get; set; }
        public string Region { get; set; }
        public int CountryId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone2 { get; set; }
        public string OtherContactInfo { get; set; }
        public string Serial { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public string RegistrationAddress { get; set; }
    
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation> Donation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TakingAnimal> TakingAnimal { get; set; }
    }
}
