using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework2.Models
{
    [Table("materia", Schema = "desarrollo")]
    public class Materia
    {
        [Key]
        public string sigla { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public virtual ICollection<MateriaCursada> materiasCursadas { get; set; }
    }
}
