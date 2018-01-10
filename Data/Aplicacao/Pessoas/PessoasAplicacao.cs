using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoWeb1.Data.Contrato;
using ProjetoWeb1.Data.Model;
using System.Data;

namespace ProjetoWeb1.Data.Aplicacao.PessoasApl
{

    public class PessoasAplicacao
    {
        private readonly IRepositorioPessoas<Pessoas> repositorio;

        public PessoasAplicacao(IRepositorioPessoas<Pessoas> repo)
        {
            repositorio = repo;
        }

        public void Salvar(Pessoas pessoa)
        {
            repositorio.Salvar(pessoa);
        }

        public void Excluir(int id)
        {
            repositorio.Excluir(id);
        }

        public IEnumerable<Pessoas> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public IEnumerable<Pessoas> ListarTodos(string where)
        {
            return repositorio.ListarTodos(where);
        }

        public Pessoas BuscaPessoa(int id)
        {
            return repositorio.BuscaPessoa(id);
        }

        public string ListarIdades()
        {
            return repositorio.ListarIdades();
        }

       

    }
}