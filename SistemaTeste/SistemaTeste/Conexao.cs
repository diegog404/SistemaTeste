using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace SistemaTeste
{
    class Conexao
    {
        public string conect = "SERVER=localhost; DATABASE=teste; UID=root; PWD=; PORT=";

        public MySqlConnection con = null;

        //abrir conexao
        public void AbrirConexao()
        {
            //testar conexao
            try
            {
                con = new MySqlConnection(conect);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na conexão: " + ex.Message);
                con.Close();
            }
        }

        //fechar conexao
        public void FecharConexao()
        {
            try
            {
                con = new MySqlConnection(conect);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na conexão: " + ex.Message);
            }
        }
    }
}
