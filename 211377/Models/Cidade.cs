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
    internal class Cidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }


        public void insert()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("INSERT INTO cidades (nome, uf) VALUES (@nome, @uf)", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@nome", nome);
                Banco.comando.Parameters.AddWithValue("@uf", uf);

                Banco.comando.ExecuteNonQuery();

                Banco.fecharConexao();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void update()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("UPDATE cidades set nome=@nome, uf=@uf WHERE id = @id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@nome", nome);
                Banco.comando.Parameters.AddWithValue("@uf", uf);
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

                Banco.comando = new MySqlCommand("DELETE FROM cidades WHERE id = @id", Banco.conexao);
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

                Banco.comando = new MySqlCommand("SELECT * FROM cidades WHERE nome like @nome "+
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
