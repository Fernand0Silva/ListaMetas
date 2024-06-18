using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd_Tarefa.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public  string nome { get; set; }
        public string Meta { get; set;}
        public float Valor { get; set;}
        public float Tempo { get; set;}
        public string Urgencia { get; set;}

    }
}