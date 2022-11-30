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
    public partial class FRMClientes : Form
    {
        Cidade ci;
        Cliente cl;
        public FRMClientes()
        {
            InitializeComponent();
        }

        void limpaControles()
        {
            txtID.Clear();
            txtNome.Clear();
            cboCidades.SelectedIndex = -1;
            txtUF.Clear();
            txtCPF.Clear();
            txtRenda.Clear();
            data_Nasc.Value = DateTime.Now;
            picFoto.ImageLocation = "";
            checkVenda.Checked = false;
            
            txtPesquisa.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            cl = new Cliente()
            {
                nome = pesquisa
            };
            dgvClientes.DataSource = cl.consultar();
        }

        private void FRMClientes_Load(object sender, EventArgs e)
        {
            ci = new Cidade();
            cboCidades.DataSource = ci.consultar();
            cboCidades.DisplayMember = "nome";
            cboCidades.ValueMember = "id";

            limpaControles();
            carregarGrid("");

            dgvClientes.Columns["id_cidade"].Visible = false;
            dgvClientes.Columns["foto"].Visible = false;
        }

        private void upload_Click(object sender, EventArgs e)
        {
            picFoto.BackgroundImageLayout = ImageLayout.Stretch;
            ofdImage.InitialDirectory = "E:\\materias\\DES_SIST\\WindowsForm\\ProjetoVendas\\211377\\fotos_clientes\\";
            ofdImage.FileName = "";
            ofdImage.ShowDialog();
            picFoto.ImageLocation = ofdImage.FileName;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNome.Text == "") return;

                cl = new Cliente()
                {
                    nome = txtNome.Text,
                    id_cidade = (int)cboCidades.SelectedValue,
                    data_nasc = data_Nasc.Value,
                    renda = double.Parse(txtRenda.Text),
                    cpf = txtCPF.Text,
                    foto = picFoto.ImageLocation,
                    venda = checkVenda.Checked
                };
                cl.insert();

                limpaControles();
                carregarGrid("");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "") return;

                cl = new Cliente()
                {
                    id = int.Parse(txtID.Text),
                    nome = txtNome.Text,
                    id_cidade = (int)cboCidades.SelectedValue,
                    data_nasc = data_Nasc.Value,
                    renda = double.Parse(txtRenda.Text),
                    cpf = txtCPF.Text,
                    foto = picFoto.ImageLocation,
                    venda = checkVenda.Checked
                };
                cl.update();

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

                if (MessageBox.Show("Deseja mesmo excluir esse cliente do registro?", "Excluir?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cl = new Cliente()
                    {
                        id = int.Parse(txtID.Text),
                    };
                    cl.delete();

                    limpaControles();
                    carregarGrid("");
                }

                MessageBox.Show("Cliente excluido com sucesso!", "Operação bem sucedida!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            carregarGrid(txtPesquisa.Text);
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvClientes.RowCount > 0)
                {
                    txtID.Text = dgvClientes.CurrentRow.Cells["id"].Value.ToString();
                    txtNome.Text = dgvClientes.CurrentRow.Cells["nome"].Value.ToString();
                    cboCidades.SelectedValue = dgvClientes.CurrentRow.Cells["id_cidade"].Value.ToString();
                    checkVenda.Checked = (bool)dgvClientes.CurrentRow.Cells["venda"].Value;
                    txtCPF.Text = dgvClientes.CurrentRow.Cells["cpf"].Value.ToString();
                    data_Nasc.Text = dgvClientes.CurrentRow.Cells["data_nasc"].Value.ToString();
                    txtRenda.Text = dgvClientes.CurrentRow.Cells["renda"].Value.ToString();
                    picFoto.ImageLocation = dgvClientes.CurrentRow.Cells["foto"].Value.ToString();

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void cboCidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboCidades.SelectedIndex != -1)
            {
                DataRowView reg = (DataRowView)cboCidades.SelectedItem;
                txtUF.Text = reg["uf"].ToString();
            }
        }
    }
}
