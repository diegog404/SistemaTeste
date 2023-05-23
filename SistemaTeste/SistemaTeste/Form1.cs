using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTeste
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
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

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

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
    }
}
