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
    internal class Produto
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int id_categoria { get; set; }
        public int id_marca { get; set; }
        public double estoque { get; set; }
        public double valor_venda { get; set; }
        public string foto { get; set; }

        public void insert()
        {
            try
            {
                Banco.abrirConexao();

                Banco.comando = new MySqlCommand("INSERT INTO produtos (descricao, id_categoria, id_marca, estoque, valor_venda, foto) VALUES (@descricao, @id_categoria, @id_marca, @estoque, @valor_venda, @foto)", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.comando.Parameters.AddWithValue("@id_categoria", id_categoria);
                Banco.comando.Parameters.AddWithValue("@id_marca", id_marca);
                Banco.comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.comando.Parameters.AddWithValue("@valor_venda", valor_venda);
                Banco.comando.Parameters.AddWithValue("@foto", foto);

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

                Banco.comando = new MySqlCommand("UPDATE produtos set descricao=@descricao, id_categoria=@id_categoria, id_marca=@id_marca, estoque=@estoque, valor_venda=@valor_venda, foto=@foto WHERE id = @id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@descricao", descricao);
                Banco.comando.Parameters.AddWithValue("@id_categoria", id_categoria);
                Banco.comando.Parameters.AddWithValue("@id_marca", id_marca);
                Banco.comando.Parameters.AddWithValue("@estoque", estoque);
                Banco.comando.Parameters.AddWithValue("@valor_venda", valor_venda);
                Banco.comando.Parameters.AddWithValue("@foto", foto);
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

                Banco.comando = new MySqlCommand("DELETE FROM produtos WHERE id = @id", Banco.conexao);
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

                Banco.comando = new MySqlCommand("SELECT p.*, m.marca, c.categoria FROM produtos p" +
                                                 " INNER JOIN marcas m on (m.id = p.id_marca) " + 
                                                 " INNER JOIN categorias c on (c.id = p.id_categoria) " +
                                                 " WHERE p.descricao like @descricao " +
                                                                                "order by p.id", Banco.conexao);
                Banco.comando.Parameters.AddWithValue("@descricao", descricao + "%");

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
