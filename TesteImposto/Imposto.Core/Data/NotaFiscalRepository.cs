using Imposto.Core.Domain;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        public void EmitirNotaFiscal(NotaFiscal notaFiscal)
        {
            XmlSerializer xs = new XmlSerializer(typeof(NotaFiscal));

            using (StreamWriter wr = new StreamWriter(@"C:\Users\pedro\OneDrive\Documentos\net-lab\TesteImposto\TesteImposto\bin\Debug\notafiscal.xml"))
                xs.Serialize(wr, notaFiscal);

            string connectionString = ConfigurationManager.ConnectionStrings["NotaFiscalConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.P_NOTA_FISCAL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@pId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@pNumeroNotaFiscal", SqlDbType.Int).Value = notaFiscal.NumeroNotaFiscal;
                    command.Parameters.Add("@pSerie", SqlDbType.Int).Value = notaFiscal.Serie;
                    command.Parameters.Add("@pNomeCliente", SqlDbType.VarChar).Value = notaFiscal.NomeCliente;
                    command.Parameters.Add("@pEstadoDestino", SqlDbType.VarChar).Value = notaFiscal.EstadoDestino;
                    command.Parameters.Add("@pEstadoOrigem", SqlDbType.VarChar).Value = notaFiscal.EstadoOrigem;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
