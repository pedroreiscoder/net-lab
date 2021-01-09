using Imposto.Core.Domain;
using System;
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
                connection.Open();

                int notaFiscalId;

                using (SqlCommand command = new SqlCommand("dbo.P_NOTA_FISCAL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@pId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@pNumeroNotaFiscal", SqlDbType.Int).Value = notaFiscal.NumeroNotaFiscal;
                    command.Parameters.Add("@pSerie", SqlDbType.Int).Value = notaFiscal.Serie;
                    command.Parameters.Add("@pNomeCliente", SqlDbType.VarChar).Value = notaFiscal.NomeCliente;
                    command.Parameters.Add("@pEstadoDestino", SqlDbType.VarChar).Value = notaFiscal.EstadoDestino;
                    command.Parameters.Add("@pEstadoOrigem", SqlDbType.VarChar).Value = notaFiscal.EstadoOrigem;
                    
                    command.ExecuteNonQuery();

                    notaFiscalId = Convert.ToInt32(command.Parameters["@pId"].Value);
                }

                using (SqlCommand command = new SqlCommand("dbo.P_NOTA_FISCAL_ITEM", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@pIdNotaFiscal", SqlDbType.Int).Value = notaFiscalId;
                    command.Parameters.Add("@pCfop", SqlDbType.VarChar);
                    command.Parameters.Add("@pTipoIcms", SqlDbType.VarChar);
                    command.Parameters.Add("@pBaseIcms", SqlDbType.Decimal);
                    command.Parameters.Add("@pAliquotaIcms", SqlDbType.Decimal);
                    command.Parameters.Add("@pValorIcms", SqlDbType.Decimal);
                    command.Parameters.Add("@pNomeProduto", SqlDbType.VarChar);
                    command.Parameters.Add("@pCodigoProduto", SqlDbType.VarChar);

                    foreach (NotaFiscalItem item in notaFiscal.ItensDaNotaFiscal)
                    {
                        command.Parameters["@pCfop"].Value = item.Cfop;
                        command.Parameters["@pTipoIcms"].Value = item.TipoIcms;
                        command.Parameters["@pBaseIcms"].Value = item.BaseIcms;
                        command.Parameters["@pAliquotaIcms"].Value = item.AliquotaIcms;
                        command.Parameters["@pValorIcms"].Value = item.ValorIcms;
                        command.Parameters["@pNomeProduto"].Value = item.NomeProduto;
                        command.Parameters["@pCodigoProduto"].Value = item.CodigoProduto;

                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }
    }
}
