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
    
    public partial class tbl_mCOMISIONESXC_CONSULTA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_mCOMISIONESXC_CONSULTA()
        {
            this.TBL_MCOMISIONESXC_CONSULTA_ANIO = new HashSet<TBL_MCOMISIONESXC_CONSULTA_ANIO>();
        }
    
        public int CODIGOCONSULTA { get; set; }
        public Nullable<int> CODIGOCLIENTE { get; set; }
        public Nullable<int> ATENDIDO { get; set; }
        public Nullable<System.DateTime> FECHA_ATENCION { get; set; }
        public Nullable<System.DateTime> FECHA_CONSULTA { get; set; }
        public string FLAG_CORREO { get; set; }
        public Nullable<bool> CARGOTARJETA { get; set; }
        public string USUARIO_ATENCION { get; set; }
        public string OBSERVACION { get; set; }
        public Nullable<bool> CHK_TERMINOS_CONDICIONES { get; set; }
        public Nullable<bool> CHK_TARIFARIO { get; set; }
        public Nullable<System.DateTime> FECHA_OBSERVACION { get; set; }
        public Nullable<System.DateTime> FECHA_OBSERVACION_UPD { get; set; }
        public int ID_CIUDAD { get; set; }
        public int ID_OFICINA { get; set; }
        public int ESTADO { get; set; }
        public Nullable<int> CODIGO_PROCESO { get; set; }
    
        public virtual tbl_aCOMISIONESXC_UBIGEO_OFICINA tbl_aCOMISIONESXC_UBIGEO_OFICINA { get; set; }
        public virtual tbl_mCOMISIONESXC_CLIENTE tbl_mCOMISIONESXC_CLIENTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_MCOMISIONESXC_CONSULTA_ANIO> TBL_MCOMISIONESXC_CONSULTA_ANIO { get; set; }
        public virtual tbl_aCOMISIONESXC_UBIGEO_CIUDAD tbl_aCOMISIONESXC_UBIGEO_CIUDAD { get; set; }
    }
}