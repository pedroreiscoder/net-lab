using Imposto.Core.Data;
using Imposto.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Imposto.Core.Tests
{
    [TestClass]
    public class NotaFiscalService
    {
        private readonly Mock<INotaFiscalRepository> _mockRepository;
        private readonly Service.NotaFiscalService _service;

        public NotaFiscalService()
        {
            _mockRepository = new Mock<INotaFiscalRepository>();
            _service = new Service.NotaFiscalService(_mockRepository.Object);
        }

        [TestMethod]
        public void DeveriaGerarNotaFiscal1()
        {
            Pedido pedido = new Pedido()
            {
                NomeCliente = "BTG Pactual",
                EstadoOrigem = "SP",
                EstadoDestino = "SE",
                ItensDoPedido = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        NomeProduto = "Mesa",
                        CodigoProduto = "me123",
                        ValorItemPedido = 50,
                        Brinde = true
                    }
                }
            };

            _service.GerarNotaFiscal(pedido);
            _mockRepository.Verify(x => x.EmitirNotaFiscalXML(It.Is<NotaFiscal>(nt => nt.NomeCliente == pedido.NomeCliente &&
                                                                                      nt.EstadoOrigem == pedido.EstadoOrigem &&
                                                                                      nt.EstadoDestino == pedido.EstadoDestino &&
                                                                                      nt.ItensDaNotaFiscal[0].Cfop == "6.009" &&
                                                                                      nt.ItensDaNotaFiscal[0].TipoIcms == "60" &&
                                                                                      nt.ItensDaNotaFiscal[0].AliquotaIcms == 0.18 &&
                                                                                      nt.ItensDaNotaFiscal[0].BaseIcms == 45 &&
                                                                                      nt.ItensDaNotaFiscal[0].ValorIcms == 8.1 &&
                                                                                      nt.ItensDaNotaFiscal[0].BaseIpi == 50 &&
                                                                                      nt.ItensDaNotaFiscal[0].AliquotaIpi == 0 &&
                                                                                      nt.ItensDaNotaFiscal[0].ValorIpi == 0 &&
                                                                                      nt.ItensDaNotaFiscal[0].NomeProduto == pedido.ItensDoPedido[0].NomeProduto &&
                                                                                      nt.ItensDaNotaFiscal[0].CodigoProduto == pedido.ItensDoPedido[0].CodigoProduto))
                                                                                    , Times.Once);
        }

        [TestMethod]
        public void DeveriaGerarNotaFiscal2()
        {
            Pedido pedido = new Pedido()
            {
                NomeCliente = "Bradesco",
                EstadoOrigem = "MG",
                EstadoDestino = "RJ",
                ItensDoPedido = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        NomeProduto = "Cadeira",
                        CodigoProduto = "ca123",
                        ValorItemPedido = 60,
                        Brinde = false
                    }
                }
            };

            _service.GerarNotaFiscal(pedido);
            _mockRepository.Verify(x => x.EmitirNotaFiscalXML(It.Is<NotaFiscal>(nt => nt.NomeCliente == pedido.NomeCliente &&
                                                                                      nt.EstadoOrigem == pedido.EstadoOrigem &&
                                                                                      nt.EstadoDestino == pedido.EstadoDestino &&
                                                                                      nt.ItensDaNotaFiscal[0].Cfop == "6.000" &&
                                                                                      nt.ItensDaNotaFiscal[0].TipoIcms == "10" &&
                                                                                      nt.ItensDaNotaFiscal[0].AliquotaIcms == 0.17 &&
                                                                                      nt.ItensDaNotaFiscal[0].BaseIcms == 60 &&
                                                                                      nt.ItensDaNotaFiscal[0].ValorIcms == 10.200000000000001 &&
                                                                                      nt.ItensDaNotaFiscal[0].BaseIpi == 60 &&
                                                                                      nt.ItensDaNotaFiscal[0].AliquotaIpi == 0.10 &&
                                                                                      nt.ItensDaNotaFiscal[0].ValorIpi == 6 &&
                                                                                      nt.ItensDaNotaFiscal[0].NomeProduto == pedido.ItensDoPedido[0].NomeProduto &&
                                                                                      nt.ItensDaNotaFiscal[0].CodigoProduto == pedido.ItensDoPedido[0].CodigoProduto &&
                                                                                      nt.ItensDaNotaFiscal[0].Desconto == 0.10))
                                                                                    , Times.Once);
        }

        [TestMethod]
        public void DeveriaGerarNotaFiscal3()
        {
            Pedido pedido = new Pedido()
            {
                NomeCliente = "Itaú",
                EstadoOrigem = "SP",
                EstadoDestino = "RO",
                ItensDoPedido = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        NomeProduto = "Garrafa",
                        CodigoProduto = "ga123",
                        ValorItemPedido = 75,
                        Brinde = true
                    }
                }
            };

            _service.GerarNotaFiscal(pedido);
            _mockRepository.Verify(x => x.EmitirNotaFiscalXML(It.Is<NotaFiscal>(nt => nt.NomeCliente == pedido.NomeCliente &&
                                                                                      nt.EstadoOrigem == pedido.EstadoOrigem &&
                                                                                      nt.EstadoDestino == pedido.EstadoDestino &&
                                                                                      nt.ItensDaNotaFiscal[0].Cfop == "6.006" &&
                                                                                      nt.ItensDaNotaFiscal[0].TipoIcms == "60" &&
                                                                                      nt.ItensDaNotaFiscal[0].AliquotaIcms == 0.18 &&
                                                                                      nt.ItensDaNotaFiscal[0].BaseIcms == 75 &&
                                                                                      nt.ItensDaNotaFiscal[0].ValorIcms == 13.5 &&
                                                                                      nt.ItensDaNotaFiscal[0].BaseIpi == 75 &&
                                                                                      nt.ItensDaNotaFiscal[0].AliquotaIpi == 0 &&
                                                                                      nt.ItensDaNotaFiscal[0].ValorIpi == 0 &&
                                                                                      nt.ItensDaNotaFiscal[0].NomeProduto == pedido.ItensDoPedido[0].NomeProduto &&
                                                                                      nt.ItensDaNotaFiscal[0].CodigoProduto == pedido.ItensDoPedido[0].CodigoProduto))
                                                                                    , Times.Once);
        }
    }
}
