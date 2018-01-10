using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ProjetoWeb1.Data.Contrato
{
    public interface IRepositorioPessoas<T> where T : class
    {
        void Salvar(T entidade);

        void Excluir(int id);

        IEnumerable<T> ListarTodos();

        IEnumerable<T> ListarTodos(string where);


        string ListarIdades();


        T BuscaPessoa(int id);
    }
}