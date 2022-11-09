using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _211377.Models;

namespace _211377.Views
{
    public partial class FRMCidades : Form
    {
        Cidade c;

        public FRMCidades()
        {
            InitializeComponent();
        }

        void limpaControles()
        {
            txtID.Clear();
            txtCidades.Clear();
            txtPesquisa.Clear();
            txtUF.Clear();
        }

        void carregarGrid(string pesquisa)
        {
            c = new Cidade()
            {
                nome = pesquisa
            };
            dgvCidades.DataSource = c.consultar();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRMCidades_Load(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCidades.Text == String.Empty) return;

                c = new Cidade()
                {
                    nome = txtCidades.Text,
                    uf = txtUF.Text
                };
                c.insert();

                limpaControles();
                carregarGrid("");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvCidades.RowCount > 0)
            {
                txtID.Text = dgvCidades.CurrentRow.Cells["id"].Value.ToString();
                txtCidades.Text = dgvCidades.CurrentRow.Cells["nome"].Value.ToString();
                txtUF.Text = dgvCidades.CurrentRow.Cells["uf"].Value.ToString();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == String.Empty) return;

                c = new Cidade()
                {
                    id = int.Parse(txtID.Text),
                    nome = txtCidades.Text,
                    uf = txtUF.Text
                };
                c.update();

                limpaControles();
                carregarGrid("");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == String.Empty) return;

                if(MessageBox.Show("Deseja mesmo excluir essa cidade do registro?", "Excluir?", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    c = new Cidade()
                    {
                        id = int.Parse(txtID.Text),
                    };
                    c.delete();

                    limpaControles();
                    carregarGrid("");
                }

                MessageBox.Show("Cidade excluida com sucesso!", "Operação bem sucedida!",
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

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limpaControles();
            carregarGrid("");
            dgvCidades.SelectedRows.Equals(0);
        }
    }
}
