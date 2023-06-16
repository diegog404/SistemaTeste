using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlConnector.Authentication;
using System.IO;

namespace SistemaTeste
{
    public partial class FrmCadastroCliente : Form
    {
        Conexao conexao = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;

        //var que recebe a img
        string foto;

        //var que recebe um cpf já cadastrado
        string cpfAntigo;

        public FrmCadastroCliente()
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
            Grid.Columns[6].HeaderText = "Foto:";

            Grid.Columns[6].Visible = false;
        }

        private void ListarGrid()
        {
            conexao.AbrirConexao();
            sql = "Select * FROM cliente ORDER BY ID ASC";

            cmd = new MySqlCommand(sql, conexao.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            da.Fill(dt);
            Grid.DataSource = dt;
            conexao.FecharConexao();

            FormatarGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //carrega tudo ao abrir
            ListarGrid();
            LimparFoto();
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
            sql = "INSERT INTO cliente (nome, endereco, cpf, telefone, imagem) VALUES(@nome, @endereco, @cpf, @telefone, @imagem)";
            cmd = new MySqlCommand(sql, conexao.con);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("@imagem", image);

            //verificar existencia do CPF
            MySqlCommand cmdVerif = new MySqlCommand("SELECT * FROM cliente WHERE cpf = @cpf", conexao.con);

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerif;
            cmdVerif.Parameters.AddWithValue("@cpf", txtCpf.Text);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("CPF já cadastrado", "CPF Existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpf.Text = "";
                txtCpf.Focus();
                return;
            }

            cmd.ExecuteNonQuery();
            conexao.FecharConexao();            

            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();

            //Atualiza a grid pelo método
            LimparFoto();
            ListarGrid();
            MessageBox.Show("Cliente cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja excluir o cliente?", "Deletar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(result == DialogResult.Yes)
            {
                conexao.AbrirConexao();
                sql = "DELETE FROM cliente WHERE id = @id";
                cmd = new MySqlCommand(sql, conexao.con);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                conexao.FecharConexao();

                DesabilitarBotoes();
                DesabilitarCampos();
                LimparCampos();

                ListarGrid();
                MessageBox.Show("Cliente deletado com sucesso!", "Cliente deletado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            btnAlterar.Enabled = true;
        }

        private void HabilitarBotoes()
        {
            btnNovo.Enabled = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = true;
            btnAlterar.Enabled = true;
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
            sql = "UPDATE cliente SET nome = @nome, endereco = @endereco, cpf = @cpf, telefone = @telefone WHERE id = @id";
            cmd = new MySqlCommand(sql, conexao.con);


            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCpf.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);

            //verificar alteração de CPF existente

            if(txtCpf.Text != cpfAntigo)
            {
                MySqlCommand cmdVerif = new MySqlCommand("SELECT * FROM cliente WHERE cpf = @cpf", conexao.con);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerif;
                cmdVerif.Parameters.AddWithValue("@cpf", txtCpf.Text);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("CPF já cadastrado", "CPF Existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCpf.Text = "";
                    txtCpf.Focus();
                    return;
                }
            }            

            cmd.ExecuteNonQuery();
            conexao.FecharConexao();            

            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();

            //Atualiza a grid pelo método
            ListarGrid();
            MessageBox.Show("Cliente alterado com sucesso!", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LimparFoto();

            HabilitarBotoes();
            btnSalvar.Enabled = false;
            HabilitarCampos();

            id = Grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = Grid.CurrentRow.Cells[1].Value.ToString();
            txtEndereco.Text = Grid.CurrentRow.Cells[2].Value.ToString();
            txtCpf.Text = Grid.CurrentRow.Cells[3].Value.ToString();
            txtTelefone.Text = Grid.CurrentRow.Cells[4].Value.ToString();

            cpfAntigo = Grid.CurrentRow.Cells[3].Value.ToString();

            /*
            if (e.RowIndex > -1)
            {
                //carrega a foto de um registro salvo
                
                if (Grid.CurrentRow.Cells[6].Value != DBNull.Value)
                {
                    byte[] imageLoad = (byte[])Grid.Rows[e.RowIndex].Cells[6].Value;
                    MemoryStream ms = new MemoryStream(imageLoad);

                    image.Image = System.Drawing.Image.FromStream(ms);
                }
                else
                {
                    image.Image = Properties.Resources.perfil;
                }
            }
            else
            {
                return;
            }
                
            }
            */
        }

        private void BuscarNome()
        {
            conexao.AbrirConexao();
            sql = "SELECT * FROM cliente WHERE nome LIKE @nome ORDER BY id ASC"; //LIKE faz uma busca nome por aproximação
            cmd = new MySqlCommand(sql, conexao.con);
            cmd.Parameters.AddWithValue("@nome", txtBusca.Text + "%"); //operador LIKE

            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            da.Fill(dt);
            Grid.DataSource = dt;
            conexao.FecharConexao();

            FormatarGrid();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void btnImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens(*.jpg; *.png) | *.jpg; *.png"; //mostra apenas jpg e png

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString(); //pega o caminho da imagem
                image.ImageLocation = foto;
            }
        }

        //envia imagem ao banco
        private byte[] Img()
        {
            if(foto == "")
            {
                return null;
            }

            byte[] imgByte = null;
            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);
            imgByte = br.ReadBytes((int)fs.Length);

            return imgByte;
        }

        //metodo limpar foto
        private void LimparFoto()
        {
            image.Image = Properties.Resources.perfil;
            foto = "ft/perfil.png";
        }
    }
}
