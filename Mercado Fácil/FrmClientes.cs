using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mercado_Fácil
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();

            this.Text = "Mercado Fácil - Clientes";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(850, 550);
            this.BackColor = System.Drawing.Color.White;

            Label titulo = new Label();
            titulo.Text = "CADASTRO DE CLIENTES";
            titulo.Font = new System.Drawing.Font(
                "Segoe UI", 18, System.Drawing.FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new System.Drawing.Point(25, 20);
            this.Controls.Add(titulo);

            CriarLabel("Código:", 30, 90);
            CriarLabel("Nome:", 30, 140);
            CriarLabel("Telefone:", 30, 190);
            CriarLabel("Código do endereço:", 30, 240);

            CriarCampo("txtCodigo", 200, 90, true);
            CriarCampo("txtNome", 200, 140);
            CriarCampo("txtTelefone", 200, 190);
            CriarCampo("txtEnderecoCodigo", 200, 240);

            CriarBotaoCliente("Salvar", "btnSalvar", 30, 310);
            this.Controls["btnSalvar"].Click += (sender, e) => SalvarCliente();
            CriarBotaoCliente("Consultar", "btnConsultar", 170, 310);
            CriarBotaoCliente("Alterar", "btnAlterar", 310, 310);
            CriarBotaoCliente("Excluir", "btnExcluir", 450, 310);
            CriarBotaoCliente("Limpar", "btnLimpar", 590, 310);
        }
        private void CriarLabel(string texto, int x, int y)
        {
            Label label = new Label();
            label.Text = texto;
            label.Location = new System.Drawing.Point(x, y);
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Segoe UI", 10);
            this.Controls.Add(label);
        }

        private void CriarCampo(
            string nome, int x, int y, bool somenteLeitura = false)
        {
            TextBox campo = new TextBox();
            campo.Name = nome;
            campo.Location = new System.Drawing.Point(x, y);
            campo.Size = new System.Drawing.Size(250, 25);
            campo.ReadOnly = somenteLeitura;
            this.Controls.Add(campo);
        }

        private void CriarBotaoCliente(
            string texto, string nome, int x, int y)
        {
            Button botao = new Button();
            botao.Name = nome;
            botao.Text = texto;
            botao.Location = new System.Drawing.Point(x, y);
            botao.Size = new System.Drawing.Size(120, 40);
            botao.Font = new System.Drawing.Font("Segoe UI", 9);
            this.Controls.Add(botao);
        }

        private void SalvarCliente()
        {
            if (string.IsNullOrWhiteSpace(
                this.Controls["txtNome"].Text))
            {
                MessageBox.Show("Informe o nome do cliente.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                this.Controls["txtTelefone"].Text))
            {
                MessageBox.Show("Informe o telefone do cliente.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                this.Controls["txtEnderecoCodigo"].Text))
            {
                MessageBox.Show("Informe o código do endereço.");
                return;
            }

            if (!int.TryParse(
                this.Controls["txtEnderecoCodigo"].Text,
                out int enderecoCodigo))
            {
                MessageBox.Show("O código do endereço deve ser um número inteiro.");
                return;
            }

            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO cliente
                           (nome, telefone, enderecoCodigo)
                           VALUES
                           (@nome, @telefone, @enderecoCodigo)";

                using (var comando = new MySql.Data.MySqlClient.MySqlCommand(
                        sql, conexao))
                    {
                        comando.Parameters.AddWithValue(
                            "@nome", this.Controls["txtNome"].Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@telefone", this.Controls["txtTelefone"].Text.Trim());

                        comando.Parameters.AddWithValue(
                            "@enderecoCodigo", enderecoCodigo);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cliente cadastrado com sucesso!");
                this.Controls["txtCodigo"].Text = "";
                this.Controls["txtNome"].Text = "";
                this.Controls["txtTelefone"].Text = "";
                this.Controls["txtEnderecoCodigo"].Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível cadastrar o cliente.\n" + ex.Message);
            }
        }
    }
}