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
    public partial class FrmMenuAdministrador : Form
    {
        public FrmMenuAdministrador()
        {
            InitializeComponent();

            this.Text = "Mercado Fácil - Administrador";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1000, 600);
            this.BackColor = System.Drawing.Color.White;

            Label titulo = new Label();
            titulo.Text = "MERCADO FÁCIL - ADMINISTRADOR";
            titulo.Font = new System.Drawing.Font(
                "Segoe UI", 18, System.Drawing.FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new System.Drawing.Point(30, 25);
            this.Controls.Add(titulo);

            CriarBotao("Produtos", "btnProdutos", 30, 100);
            CriarBotao("Clientes", "btnClientes", 260, 100);
            CriarBotao("Entregadores", "btnEntregadores", 490, 100);
            CriarBotao("Endereços", "btnEnderecos", 30, 180);
            CriarBotao("Encomendas", "btnEncomendas", 260, 180);
            CriarBotao("Roteiros de entrega", "btnRoteiros", 490, 180);
            CriarBotao("Tipos de veículo", "btnVeiculos", 30, 260);
            CriarBotao("Usuários", "btnUsuarios", 260, 260);
        }

        private void CriarBotao(string texto, string nome, int x, int y)
        {
            Button botao = new Button();

            botao.Name = nome;
            botao.Text = texto;
            botao.Size = new System.Drawing.Size(200, 50);
            botao.Location = new System.Drawing.Point(x, y);
            botao.Font = new System.Drawing.Font("Segoe UI", 10);
            botao.BackColor = System.Drawing.Color.FromArgb(30, 90, 160);
            botao.ForeColor = System.Drawing.Color.White;
            botao.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(botao);

            if (nome == "btnClientes")
            {
                botao.Click += (sender, e) =>
                {
                    using (FrmClientes tela = new FrmClientes())
                    {
                        tela.ShowDialog();
                    }
                };
            }

            this.Controls.Add(botao);
        }
    }
}