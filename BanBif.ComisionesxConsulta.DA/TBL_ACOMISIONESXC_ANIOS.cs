//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanBif.ComisionesxConsulta.DA
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ACOMISIONESXC_ANIOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_ACOMISIONESXC_ANIOS()
        {
            this.TBL_MCOMISIONESXC_CONSULTA_ANIO = new HashSet<TBL_MCOMISIONESXC_CONSULTA_ANIO>();
        }
    
        public int CODIGO_ANIO { get; set; }
        public string ANIO { get; set; }
        public int ESTADO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_MCOMISIONESXC_CONSULTA_ANIO> TBL_MCOMISIONESXC_CONSULTA_ANIO { get; set; }
    }
}
