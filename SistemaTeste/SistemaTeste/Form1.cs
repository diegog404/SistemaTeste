using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlConnector.Authentication;

namespace SistemaTeste
{
    public partial class FrmPrincipal : Form
    {
        Conexao conexao = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FormatarGrid()
        {
            Grid.Columns[0].HeaderText = "Código:";
            Grid.Columns[1].HeaderText = "Nome:";
            Grid.Columns[2].HeaderText = "End.:";
            Grid.Columns[3].HeaderText = "Cpf:";
            Grid.Columns[4].HeaderText = "Telefone:";
            Grid.Columns[5].HeaderText = "Celular:";
        }

        private void ListarGrid()
        {
            conexao.AbrirConexao();
            sql = "Select * FROM 'cliente' ORDER BY NOME ASC";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesabilitarBotoes();
            DesabilitarCampos();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            HabilitarBotoes();
            HabilitarCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite o nome");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCpf.Text == "   .   .   -" || txtCpf.Text.Length < 14)
            {
                MessageBox.Show("Digite o cpf");
                txtCpf.Text = "";
                txtCpf.Focus();
                return;
            }

            conexao.AbrirConexao();
            sql = "INSERT INTO cliente (nome, endereco, cpf, telefone) VALUES(@nome, @endereco, @cpf, @telefone)";
            cmd = new MySqlCommand(sql, conexao.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.ExecuteNonQuery();
            conexao.FecharConexao();

            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();
        }

        private void DesabilitarBotoes()
        {
            btnNovo.Enabled = true;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void HabilitarBotoes()
        {
            btnNovo.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            txtCpf.Enabled = false;
            txtTelefone.Enabled = false;
        }

        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            txtCpf.Enabled = true;
            txtTelefone.Enabled = true;

            txtNome.Focus();
            txtNome.Text = " ";
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEndereco.Clear();
            txtCpf.Clear();
            txtTelefone.Clear();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite o nome");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCpf.Text == "   .   .   -" || txtCpf.Text.Length < 14)
            {
                MessageBox.Show("Digite o cpf");
                txtCpf.Text = "";
                txtCpf.Focus();
                return;
            }

            conexao.AbrirConexao();
            sql = "UPDATE cliente SET nome = @nome, endereco = @endereco, cpf = @cpf, telefone = @telefone";
            cmd = new MySqlCommand(sql, conexao.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.ExecuteNonQuery();
            conexao.FecharConexao();

            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();
        }
    }
}
