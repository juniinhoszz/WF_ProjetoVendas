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
    internal class Marca
    {
        public int id { get; set; }
        public string marca { get; set; }


        public void insert()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("INSERT INTO marcas (marca) VALUES (@marca)", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@marca", marca);

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

                Banco.comando = new MySqlCommand("UPDATE marcas set marca=@marca WHERE id = @id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@marca", marca);
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

                Banco.comando = new MySqlCommand("DELETE FROM marcas WHERE id = @id", Banco.conexao);
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

                Banco.comando = new MySqlCommand("SELECT * FROM marcas WHERE marca like @marca " +
                                                                                "order by id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@marca", marca + "%");

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

