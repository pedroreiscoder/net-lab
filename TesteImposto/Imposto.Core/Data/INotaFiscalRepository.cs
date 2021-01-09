using Imposto.Core.Domain;

namespace Imposto.Core.Data
{
    public interface INotaFiscalRepository
    {
        void EmitirNotaFiscal(NotaFiscal notaFiscal);
    }
}
