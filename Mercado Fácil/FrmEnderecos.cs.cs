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
    public partial class FrmEnderecos : Form
    {
        public FrmEnderecos()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogradouro.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtBairro.Text) ||
                string.IsNullOrWhiteSpace(txtCidade.Text) ||
                string.IsNullOrWhiteSpace(txtEstado.Text) ||
                string.IsNullOrWhiteSpace(txtPais.Text) ||
                string.IsNullOrWhiteSpace(txtCep.Text))
            {
                MessageBox.Show(
                    "Preencha todos os campos obrigatórios.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!int.TryParse(txtNumero.Text, out int numero))
            {
                MessageBox.Show("Digite um número válido para o endereço.");
                txtNumero.Focus();
                return;
            }

            if (!long.TryParse(txtCep.Text, out long cep))
            {
                MessageBox.Show("Digite um CEP válido, somente com números.");
                txtCep.Focus();
                return;
            }

            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();

                    string sql = @"INSERT INTO endereco
                (logradouro, numero, complemento, bairro,
                 cidade, estado, pais, cep)
                VALUES
                (@logradouro, @numero, @complemento, @bairro,
                 @cidade, @estado, @pais, @cep)";

                    using (var comando = new MySql.Data.MySqlClient.MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@logradouro", txtLogradouro.Text.Trim());
                        comando.Parameters.AddWithValue("@numero", numero);
                        comando.Parameters.AddWithValue("@complemento", txtComplemento.Text.Trim());
                        comando.Parameters.AddWithValue("@bairro", txtBairro.Text.Trim());
                        comando.Parameters.AddWithValue("@cidade", txtCidade.Text.Trim());
                        comando.Parameters.AddWithValue("@estado", txtEstado.Text.Trim());
                        comando.Parameters.AddWithValue("@pais", txtPais.Text.Trim());
                        comando.Parameters.AddWithValue("@cep", cep);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Endereço cadastrado com sucesso!",
                    "Mercado Fácil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível salvar o endereço:\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

    }
}

