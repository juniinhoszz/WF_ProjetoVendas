using _211377.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _211377.Views
{
    public partial class FRMProdutos : Form
    {
        Produto p;
        Categoria ca;
        Marca ma;
        public FRMProdutos()
        {
            InitializeComponent();
        }

        void limpaControles()
        {
            txtID.Clear();
            txtDesc.Clear();
            cboCategorias.SelectedIndex = -1;
            cboMarcas.SelectedIndex = -1;
            txtVlrVenda.Clear();
            txtEstoque.Clear();
            picFoto.ImageLocation = "";
            txtPesquisa.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            p = new Produto()
            {
                descricao = pesquisa
            };
            dgvProdutos.DataSource = p.consultar();
        }


        private void FRMProdutos_Load(object sender, EventArgs e)
        {
            ca = new Categoria();
            cboCategorias.DataSource = ca.consultar();
            cboCategorias.DisplayMember = "categoria";
            cboCategorias.ValueMember = "id";

            ma = new Marca();
            cboMarcas.DataSource = ma.consultar();
            cboMarcas.DisplayMember = "marca";
            cboMarcas.ValueMember = "id";

            limpaControles();
            carregarGrid("");

            dgvProdutos.Columns["id_categoria"].Visible = false;
            dgvProdutos.Columns["id_marca"].Visible = false;
            dgvProdutos.Columns["foto"].Visible = false;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDesc.Text == "") return;

                p = new Produto()
                {
                    descricao = txtDesc.Text,
                    id_categoria = (int)cboCategorias.SelectedValue,
                    id_marca = (int)cboMarcas.SelectedValue,
                    valor_venda = double.Parse(txtVlrVenda.Text),
                    estoque = double.Parse(txtEstoque.Text),
                    foto = picFoto.ImageLocation,
                };
                p.insert();

                limpaControles();
                carregarGrid("");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "") return;

                p = new Produto()
                {
                    id = int.Parse(txtID.Text),
                    descricao = txtDesc.Text,
                    id_categoria = (int)cboCategorias.SelectedValue,
                    id_marca = (int)cboMarcas.SelectedValue,
                    valor_venda = double.Parse(txtVlrVenda.Text),
                    estoque = double.Parse(txtEstoque.Text),
                    foto = picFoto.ImageLocation,
                };
                p.update();

                limpaControles();
                carregarGrid("");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == String.Empty) return;

                if (MessageBox.Show("Deseja mesmo excluir esse Produto do registro?", "Excluir?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    p = new Produto()
                    {
                        id = int.Parse(txtID.Text),
                    };
                    p.delete();

                    limpaControles();
                    carregarGrid("");
                }

                MessageBox.Show("Produto excluido com sucesso!", "Operação bem sucedida!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void upload_Click(object sender, EventArgs e)
        {
            picFoto.BackgroundImageLayout = ImageLayout.Stretch;
            ofdImage.InitialDirectory = "E:\\materias\\DES_SIST\\WindowsForm\\ProjetoVendas\\211377\\fotos_clientes\\";
            ofdImage.FileName = "";
            ofdImage.ShowDialog();
            picFoto.ImageLocation = ofdImage.FileName;
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvProdutos.RowCount > 0)
                {
                    txtID.Text = dgvProdutos.CurrentRow.Cells["id"].Value.ToString();
                    txtDesc.Text = dgvProdutos.CurrentRow.Cells["descricao"].Value.ToString();
                    cboCategorias.SelectedValue = dgvProdutos.CurrentRow.Cells["id_categoria"].Value.ToString();
                    cboMarcas.SelectedValue = dgvProdutos.CurrentRow.Cells["id_marca"].Value.ToString();
                    txtVlrVenda.Text = dgvProdutos.CurrentRow.Cells["valor_venda"].Value.ToString();
                    txtEstoque.Text = dgvProdutos.CurrentRow.Cells["estoque"].Value.ToString();
                    picFoto.ImageLocation = dgvProdutos.CurrentRow.Cells["foto"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
