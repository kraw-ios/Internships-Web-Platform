using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EstagiosDEIS.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        static int incID = 1;

        
        public int IDEmpresa { get; set; } //Verificar a implementação de ID's em C#
        public String Nome { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        public Empresa() {
            IDEmpresa = incID;
            incID++;
        }

    }
}