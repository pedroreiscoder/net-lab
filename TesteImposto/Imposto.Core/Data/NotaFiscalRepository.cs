using Imposto.Core.Domain;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        public void EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();

            notaFiscal.NumeroNotaFiscal = 99999;
            notaFiscal.Serie = new Random().Next(int.MaxValue);
            notaFiscal.NomeCliente = pedido.NomeCliente;
            notaFiscal.EstadoDestino = pedido.EstadoDestino;
            notaFiscal.EstadoOrigem = pedido.EstadoOrigem;

            NotaFiscalItem notaFiscalItem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                notaFiscalItem = new NotaFiscalItem();

                if (notaFiscal.EstadoOrigem == "SP")
                {
                    switch (notaFiscal.EstadoDestino)
                    {
                        case "RJ":
                            notaFiscalItem.Cfop = "6.000";
                            break;
                        case "PE":
                            notaFiscalItem.Cfop = "6.001";
                            break;
                        case "MG":
                            notaFiscalItem.Cfop = "6.002";
                            break;
                        case "PB":
                            notaFiscalItem.Cfop = "6.003";
                            break;
                        case "PR":
                            notaFiscalItem.Cfop = "6.004";
                            break;
                        case "PI":
                            notaFiscalItem.Cfop = "6.005";
                            break;
                        case "RO":
                            notaFiscalItem.Cfop = "6.006";
                            break;
                        case "TO":
                            notaFiscalItem.Cfop = "6.008";
                            break;
                        case "SE":
                            notaFiscalItem.Cfop = "6.009";
                            break;
                        case "PA":
                            notaFiscalItem.Cfop = "6.010";
                            break;
                    }
                }

                if (notaFiscal.EstadoOrigem == "MG")
                {
                    switch (notaFiscal.EstadoDestino)
                    {
                        case "RJ":
                            notaFiscalItem.Cfop = "6.000";
                            break;
                        case "PE":
                            notaFiscalItem.Cfop = "6.001";
                            break;
                        case "MG":
                            notaFiscalItem.Cfop = "6.002";
                            break;
                        case "PB":
                            notaFiscalItem.Cfop = "6.003";
                            break;
                        case "PR":
                            notaFiscalItem.Cfop = "6.004";
                            break;
                        case "PI":
                            notaFiscalItem.Cfop = "6.005";
                            break;
                        case "RO":
                            notaFiscalItem.Cfop = "6.006";
                            break;
                        case "TO":
                            notaFiscalItem.Cfop = "6.008";
                            break;
                        case "SE":
                            notaFiscalItem.Cfop = "6.009";
                            break;
                        case "PA":
                            notaFiscalItem.Cfop = "6.010";
                            break;
                    }
                }

                if (notaFiscal.EstadoOrigem == notaFiscal.EstadoDestino || itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }

                if (notaFiscalItem.Cfop == "6.009")
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido * 0.90; //redução de base
                else
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;

                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            XmlSerializer xs = new XmlSerializer(typeof(NotaFiscal));

            using (StreamWriter wr = new StreamWriter(@"C:\Users\pedro\OneDrive\Documentos\net-lab\TesteImposto\TesteImposto\bin\Debug\notafiscal.xml"))
                xs.Serialize(wr, notaFiscal);
        }
    }
}
