using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjetoWeb1.Data.Model;
using ProjetoWeb1.Data.Aplicacao.PessoasApl;

namespace ProjetoWeb1.Controllers
{
    public class ValuesController : ApiController
    {

        private PessoasAplicacao appPessoas;

        Pessoas pessoas = new Pessoas();

        public ValuesController()
        {
            appPessoas = PessoasAplicacaoConstrutor.PessoasAplicacaoDAO();
        }


        // GET api/values
        public IEnumerable<Pessoas> Get()
        {
            return appPessoas.ListarTodos();
        }

        // GET api/values/5
        public IEnumerable<Pessoas> Get(string id)
        {
            if (id.Substring(0,1).Equals("0"))
            {
                id = " id = " + id.Substring(1, id.Length - 1).ToString();
            }
            else
                if(id.Substring(0, 1).Equals("1"))
            {
                id = " (Nome like '%"            + id.Substring(1, id.Length - 1).ToString() 
                    + "%') or (id like '%"       + id.Substring(1, id.Length - 1).ToString() 
                    + "%') or (Endereco like '%" + id.Substring(1, id.Length - 1).ToString() 
                    + "%') ";
            }
            else
                if (id.Substring(0, 1).Equals("2"))
            {
                id = " (Nome like '%" + id.Substring(1, id.Length - 1).ToString()
                     + "%') or (id like '%" + id.Substring(1, id.Length - 1).ToString()
                     + "%') or (Endereco like '%" + id.Substring(1, id.Length - 1).ToString()
                     + "%') ";
            }

            return appPessoas.ListarTodos(id);
        }

        


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Pessoas pessoas)
        {
            appPessoas.Salvar(pessoas);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            appPessoas.Excluir(id);
        }
    }
}
