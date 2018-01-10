using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjetoWeb1.Data.ContextoData;
using ProjetoWeb1.Data.Model;
using ProjetoWeb1.Data.Contrato;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
using System.Data;

namespace ProjetoWeb1.Data.DAO
{
    public class PessoasDAO : IRepositorioPessoas<Pessoas>
    {
        private Contexto contexto;

        private void Inserir(Pessoas pessoa)
        {
            var strQuery = "";
            strQuery += " INSERT INTO PESSOAS (	       " +
                        //   "            (Id          " +
                        "             Nome             " +
                        "            ,Data_Nascimento  " +
                        "            ,Documento        " +
                        "            ,Endereco         " +
                        "            ,Sexo )           ";

            strQuery += string.Format(" VALUES ('{0}','{1}','{2}','{3}','{4}') ",
                                      //  pessoa.id,
                                      pessoa.nome
                                   // , DateTime.Parse(pessoa.data_nascimento).ToString("dd/MM/yyyy")
                                    , pessoa.data_nascimento.ToString()
                                    , pessoa.documento
                                    , pessoa.endereco
                                    , pessoa.sexo
                                    );

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Pessoas pessoa)
        {
            var strQuery = "";

            strQuery += " UPDATE PESSOAS SET  ";
            strQuery += string.Format(" Nome            = '{0}', ", pessoa.nome);
            // strQuery += string.Format(" Data_Nascimento = '{0}', ", pessoa.data_nascimento.ToString("dd/MM/yyyy"));
            strQuery += string.Format(" Data_Nascimento = '{0}', ", pessoa.data_nascimento.ToString());
            strQuery += string.Format(" Documento       = '{0}', ", pessoa.documento);
            strQuery += string.Format(" Endereco        = '{0}', ", pessoa.endereco);
            strQuery += string.Format(" Sexo            = '{0}'  ", pessoa.sexo);

            strQuery += string.Format(" WHERE Id        =  {0}   ", pessoa.id);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Pessoas pessoa)
        {
            if (pessoa.id > 0)
                Alterar(pessoa);
            else
                Inserir(pessoa);
        }


        public void Excluir(int id)
        {
            var strQuery = "";

            strQuery += " DELETE FROM PESSOAS   ";
            strQuery += string.Format(" WHERE Id  = {0} ", id);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }


        public IEnumerable<Pessoas> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM PESSOAS ";


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }


        public IEnumerable<Pessoas> ListarTodos(string where)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM PESSOAS where {0} ", where);


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }


        private List<Pessoas> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var pessoa = new List<Pessoas>();

            while (reader.Read())
            {
                var temObjeto = new Pessoas()
                {

                    id = int.Parse(reader["Id"].ToString()),
                    nome = reader["Nome"].ToString(),
                    data_nascimento = DateTime.Parse(reader["Data_Nascimento"].ToString()).ToString("yyyy-MM-dd"),
                    documento = reader["Documento"].ToString(),
                    endereco = reader["Endereco"].ToString(),
                    sexo = reader["Sexo"].ToString()

                };
                pessoa.Add(temObjeto);
            }
            reader.Close();
            return pessoa;
        }


        public Pessoas BuscaPessoa(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT *                 " +
                                             " FROM PESSOAS         " +
                                             " WHERE Id = '{0}' ", id);

                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

                return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
            }
        }


        //public DataTable FaixasIdades()
        //{
        //    using (contexto = new Contexto())
        //    {
        //        DataTable dt = new DataTable();

        //        var strQuery = " select '0 a 9' as Name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) <= 9  ) as idade union all "
        //           + " select '10 a 19' as Name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 10 and 19 ) as idade union all"
        //           + " select '20 a 29' as Name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 20 and 29 ) as idade union all "
        //           + " select '30 a 39' as Name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 30 and 39 ) as idade union all "
        //           + " select 'Maiores de 41' as Name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) >= 40 ) as idade ";


        //        var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

        //        dt.Load(retornoDataReader);

        //        return dt;
        //    }
        //}



        public string ListarIdades()
        {
            using (contexto = new Contexto())
            {

                DataTable dt = new DataTable();
                string json = null;


                //var strQuery = "select  "
                //             + " (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) <= 9 ) as idade1 ,             "
                //             + " (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 10 and 19 ) as idade2, "
                //             + " (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 20 and 29 ) as idade3, "
                //             + " (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 30 and 39 ) as idade4, "
                //             + " (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) >= 40 ) as idade5  ";

                var strQuery = " select '0 a 9'   as name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) <= 9  )             as votes, 0 as qtdm, 0 as qtdf union all  "
                             + " select '10 a 19' as name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 10 and 19 ) as votes, 0 as qtdm, 0 as qtdf union all  "
                             + " select '20 a 29' as name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 20 and 29 ) as votes, 0 as qtdm, 0 as qtdf union all  "
                             + " select '30 a 39' as name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) between 30 and 39 ) as votes, 0 as qtdm, 0 as qtdf union all  "
                             + " select 'Maiores de 41' as name, (select count(*) from PESSOAS where FLOOR(DATEDIFF(DAY, Data_Nascimento, GETDATE()) / 365.25) >= 40 )       as votes, 0 as qtdm, 0 as qtdf union all  "
                             + " select 'Tipo' as name, 0 as votes, (select count(*) from PESSOAS where Sexo = 'M') as qtdm, (select count(*) from PESSOAS where Sexo = 'F') as qtdf " ;


                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

                dt.Load(retornoDataReader);

                json = JsonConvert.SerializeObject(dt);


                return json;



                //var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

                //return TransformaReaderEmListaDeObjetoChart(retornoDataReader);
            }
        }

        private List<string> TransformaReaderEmListaDeObjetoChart(SqlDataReader reader)
        {
            //var chartpessoa = new ChartPessoas;
            List<string> lstPessoas = new List<string>();
            List<ChartPessoas> lstcharts = new List<ChartPessoas>();

            string json = null;



            //var JSONString = new StringBuilder();
            //    JSONString.Append("[{'cols': [{'id': 'name', 'label': 'Month', 'type': 'string'}]);

                
            //    JSONString.Append("]");





                while (reader.Read())
            {


                //var temObjeto = new ChartPessoas()
                //{
                //    //idade = "1", //reader["idade1"].ToString(),
                //    //idade2 = "10", //reader["idade2"].ToString(),
                //    //idade3 = "20", //reader["idade3"].ToString(),
                //    //idade4 = "5", //reader["idade4"].ToString(),
                //    //idade5 = "0", //reader["idade5"].ToString()
                //};

                //lstcharts.Add(temObjeto);

                //lstPessoas.Add(temObjeto);

                //    lstcharts.Add(new ChartPessoas(reader["idade1"].ToString(), reader["idade2"].ToString(), reader["idade3"].ToString(), reader["idade4"].ToString(), reader["idade5"].ToString()));

                //   lstPessoas.Add(lstcharts.OfType<string>());



                // ChartPessoas chartpessoas = new ChartPessoas();
                // chartpessoas.idade = reader["idade"].ToString();
                //   chartpessoas.idade2 = "10"; //reader["idade2"].ToString(),
                //chartpessoas.idade3 = "20"; //reader["idade3"].ToString(),
                //chartpessoas.idade4 = "5"; //reader["idade4"].ToString(),
                //chartpessoas.idade5 = "0"; //reader["idade5"].ToString()


                var temObjeto = new ChartPessoas()
                {
                    name  = reader["name"].ToString(),
                    votes = int.Parse(reader["idade"].ToString())
                };

                lstcharts.Add(temObjeto);
            }
            reader.Close();

          //  json = JsonConvert.SerializeObject(lstcharts);


            return lstPessoas;
        }



    }




}

    