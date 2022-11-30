using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _211377
{
    internal class Banco
    {
        public static MySqlConnection conexao;
        public static MySqlCommand comando;
        public static MySqlDataAdapter adaptador;
        public static DataTable dataTabela;

        public static void abrirConexao()
        {
            try
            {
                conexao = new MySqlConnection("server=localhost;port=3307;uid=root;pwd=etecjau");

                conexao.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void fecharConexao()
        {
            try
            {
                conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CriarBanco()
        {
            try
            {
                abrirConexao();

                comando = new MySqlCommand("CREATE DATABASE IF NOT EXISTS vendas; USE vendas", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS cidades " +
                                            "(id integer auto_increment primary key, " +
                                            "nome char(40), " +
                                            "uf char(2))", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS marcas " +
                                            "(id integer auto_increment primary key, " +
                                            "marca char(20))", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS categorias " +
                                            "(id integer auto_increment primary key, " +
                                            "categoria char(20))", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS clientes " +
                                            "(id integer auto_increment primary key,nome char(40), id_cidade integer,data_nasc date,renda double(10,2),cpf char(14),foto varchar(150),venda boolean);", conexao);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS produtos " +
                                            "(id integer auto_increment primary key,descricao char(40), id_categoria integer, id_marca integer, estoque double(10,3), valor_venda double(10,3) ,foto varchar(150));", conexao);
                comando.ExecuteNonQuery();

                fecharConexao();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
