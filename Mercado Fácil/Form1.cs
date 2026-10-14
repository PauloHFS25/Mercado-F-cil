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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                    string.IsNullOrWhiteSpace(txtSenha.Text))
                {
                    MessageBox.Show(
                        "Informe o usuário e a senha.",
                        "Atenção",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                using (MySql.Data.MySqlClient.MySqlConnection conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"SELECT nome, perfil
                       FROM usuario
                       WHERE usuario = @usuario
                       AND senha = @senha
                       LIMIT 1";

                    using (MySql.Data.MySqlClient.MySqlCommand comando =
                           new MySql.Data.MySqlClient.MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                        comando.Parameters.AddWithValue("@senha", txtSenha.Text);

                        using (MySql.Data.MySqlClient.MySqlDataReader leitor =
                               comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                string nome = leitor["nome"].ToString();
                                string perfil = leitor["perfil"].ToString();

                                if (perfil == "Administrador")
                                {
                                    FrmMenuAdministrador menu = new FrmMenuAdministrador();
                                    menu.Show();
                                }
                                else if (perfil == "Usuário")
                                {
                                    FrmMenuUsuario menu = new FrmMenuUsuario();
                                    menu.Show();
                                }
                                else
                                {
                                    MessageBox.Show(
                                        "Perfil de usuário não reconhecido.",
                                        "Erro",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                                    return;
                                }

                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Usuário ou senha inválidos.",
                                    "Acesso negado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao realizar o login:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
