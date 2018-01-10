﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjetoWeb1.Data.Model;
using ProjetoWeb1.Data.Aplicacao.PessoasApl;

namespace ProjetoWeb1.Controllers
{
    public class ValuesChartController : ApiController
    {
        private PessoasAplicacao appPessoas;


        public ValuesChartController()
        {
            appPessoas = PessoasAplicacaoConstrutor.PessoasAplicacaoDAO();
        }


        // GET api/<controller>
        public string Get()
        {
         //   return new string[] { "value1", "value2" };

            return appPessoas.ListarIdades();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}