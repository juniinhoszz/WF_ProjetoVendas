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
    internal class Categoria
    {
        public int id { get; set; }
        public string categoria { get; set; }


        public void insert()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("INSERT INTO categorias (categoria) VALUES (@categoria)", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@categoria", categoria);

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

                Banco.comando = new MySqlCommand("UPDATE categorias set categoria=@categoria WHERE id = @id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@categoria", categoria);
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

                Banco.comando = new MySqlCommand("DELETE FROM categorias WHERE id = @id", Banco.conexao);
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

                Banco.comando = new MySqlCommand("SELECT * FROM categorias WHERE categoria like @categoria " +
                                                                                "order by id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@categoria", categoria + "%");

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

