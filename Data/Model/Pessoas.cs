using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ProjetoWeb1.Data.Model
{
    public class Pessoas
    {

        public int id { get; set; }

        public string nome { get; set; }

        public string data_nascimento { get; set; }

        public string documento { get; set; }

        public string sexo { get; set; }

        public string endereco { get; set; }

    }
}