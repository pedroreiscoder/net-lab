using Imposto.Core.Service;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Imposto.Core.Domain;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private readonly INotaFiscalService _notaFiscalService;
        private string[] estados = new string[] { "AC", "AL", "AP", "AM", "BA", "CE", "ES", "GO", "MA", "MT", "MS", "MG", "PA", 
                                                  "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO", "DF" };

        public FormImposto(INotaFiscalService notaFiscalService)
        {
            InitializeComponent();
            _notaFiscalService = notaFiscalService;
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Pedido pedido = new Pedido();
                pedido.EstadoOrigem = txtEstadoOrigem.Text;
                pedido.EstadoDestino = txtEstadoDestino.Text;
                pedido.NomeCliente = textBoxNomeCliente.Text;

                DataTable table = (DataTable)dataGridViewPedidos.DataSource;

                foreach (DataRow row in table.Rows)
                {
                    pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            Brinde = row["Brinde"] != DBNull.Value ? Convert.ToBoolean(row["Brinde"]) : false,
                            CodigoProduto = row["Codigo do produto"].ToString(),
                            NomeProduto = row["Nome do produto"].ToString(),
                            ValorItemPedido = row["Valor"] != DBNull.Value ? Convert.ToDouble(row["Valor"]) : 0
                        });
                }

                _notaFiscalService.GerarNotaFiscal(pedido);
                MessageBox.Show("Operação efetuada com sucesso");

                textBoxNomeCliente.Clear();
                txtEstadoOrigem.Clear();
                txtEstadoDestino.Clear();

                for (int i = dataGridViewPedidos.Rows.Count - 2; i >= 0; i--)
                    dataGridViewPedidos.Rows.RemoveAt(i);
            }
        }

        private void txtEstadoOrigem_Validating(object sender, CancelEventArgs e)
        {
            if (!estados.Contains(txtEstadoOrigem.Text))
            {
                e.Cancel = true;
                txtEstadoOrigem.Focus();
                errorProvider1.SetError(txtEstadoOrigem, "Estado inválido! Utilizar a Sigla do Estado");
            }
            else
                errorProvider1.SetError(txtEstadoOrigem, "");
        }

        private void txtEstadoDestino_Validating(object sender, CancelEventArgs e)
        {
            if (!estados.Contains(txtEstadoDestino.Text))
            {
                e.Cancel = true;
                txtEstadoDestino.Focus();
                errorProvider1.SetError(txtEstadoDestino, "Estado inválido! Utilizar a Sigla do Estado");
            }
            else
                errorProvider1.SetError(txtEstadoDestino, "");
        }
    }
}
