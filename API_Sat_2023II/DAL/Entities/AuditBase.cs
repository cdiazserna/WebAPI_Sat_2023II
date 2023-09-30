using System.ComponentModel.DataAnnotations;

namespace API_Sat_2023II.DAL.Entities
{
    public class AuditBase
    {
        [Key] //DataAnnotation me sirve para definir que esta propiedad ID es un PK
        [Required] //Para campos obligatorios, o sea que deben tener un valor (no permite nulls)
        public virtual Guid Id { get; set; } //Será la PK de todas las tablas de mi BD
        public virtual DateTime? CreatedDate { get; set; } //Campos nulleables, notación elivs (?) 
        public virtual DateTime? ModifiedDate { get; set; }
    }
}
