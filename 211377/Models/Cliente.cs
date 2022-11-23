using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _211377.Models
{
    internal class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int id_cidade { get; set; }
        public DateTime data_nasc { get; set; }
        public double renda { get; set; }
        public string cpf { get; set; }
        public string foto { get; set; }
        public bool venda { get; set; }

        public string cidade { get; set; }

        public void insert()
        {
            try
            {
                Banco.abrirConexao();

                

                Banco.comando = new MySqlCommand("INSERT INTO clientes (nome, id_cidade, renda, cpf, foto, venda) VALUES (@nome, @id_cidade, @renda, @cpf, @foto, @venda)", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@nome", nome);
                Banco.comando.Parameters.AddWithValue("@id_cidade", id_cidade);
                Banco.comando.Parameters.AddWithValue("@renda", renda);
                Banco.comando.Parameters.AddWithValue("@cpf", cpf);
                Banco.comando.Parameters.AddWithValue("@foto", foto);
                Banco.comando.Parameters.AddWithValue("@venda", venda);

                Banco.comando.ExecuteNonQuery();

                Banco.fecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("UPDATE clientes set nome=@nome, id_cidade=@id_cidade, renda=@renda, cpf=@cpf, foto=@foto, venda=@venda WHERE id = @id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@nome", nome);
                Banco.comando.Parameters.AddWithValue("@id_cidade", id_cidade);
                Banco.comando.Parameters.AddWithValue("@renda", renda);
                Banco.comando.Parameters.AddWithValue("@cpf", cpf);
                Banco.comando.Parameters.AddWithValue("@foto", foto);
                Banco.comando.Parameters.AddWithValue("@venda", venda);
                Banco.comando.Parameters.AddWithValue("@id", id);

                Banco.comando.ExecuteNonQuery();

                Banco.fecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        public void delete()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("DELETE FROM clientes WHERE id = @id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@id", id);

                Banco.comando.ExecuteNonQuery();

                Banco.fecharConexao();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable consultar()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("SELECT * FROM clientes WHERE nome like @nome " +
                                                                                "order by id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@nome", nome + "%");

                Banco.adaptador = new MySqlDataAdapter(Banco.comando);
                Banco.dataTabela = new DataTable();
                Banco.adaptador.Fill(Banco.dataTabela);

                Banco.fecharConexao();
                return Banco.dataTabela;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        

    }
}

