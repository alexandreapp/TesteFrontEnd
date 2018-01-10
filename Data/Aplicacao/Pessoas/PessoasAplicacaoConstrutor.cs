using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoWeb1.Data.DAO;


namespace ProjetoWeb1.Data.Aplicacao.PessoasApl
{
    public class PessoasAplicacaoConstrutor
    {
        public static PessoasAplicacao PessoasAplicacaoDAO()
        {
            return new PessoasAplicacao(new PessoasDAO());
        }
    }
}