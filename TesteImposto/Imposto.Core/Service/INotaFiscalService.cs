using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    public interface INotaFiscalService
    {
        void GerarNotaFiscal(Pedido pedido);
    }
}
