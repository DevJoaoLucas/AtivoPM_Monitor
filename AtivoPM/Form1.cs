using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace AtivoPM
{
    public partial class FormMonitor : Form
    {
        private string connectionString;
        private bool errorDisplayed = false;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public FormMonitor()
        {
            InitializeComponent();

            LoadConnectionSettings();
            MonitorStatus();
        }

        private void AtualizarLabelsEmLoop()
        {
            Thread labelUpdateThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        VerificarStatusEmpresas();
                        Thread.Sleep(10000); // Aguarda 5 segundos antes de verificar novamente
                    }
                    catch (Exception ex)
                    {
                        if (!errorDisplayed)
                        {
                            MessageBox.Show("Erro ao atualizar as labels: " + ex.Message);
                            errorDisplayed = true;
                        }
                    }
                }
            });

            labelUpdateThread.IsBackground = true; // Define a thread como background para encerrar junto com a aplicação
            labelUpdateThread.Start();
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView.Columns[e.ColumnIndex].HeaderText == ".IS" || dataGridView.Columns[e.ColumnIndex].HeaderText == "Gateway")
                {
                    string cellValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (cellValue != "EM EXECUÇÃO")
                    {
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }

        private void LoadConnectionSettings()
        {
            string pathToXml = Path.Combine(Application.StartupPath, "BancoDados.xml");

            if (File.Exists(pathToXml))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(pathToXml);

                    XmlNode node = doc.SelectSingleNode("/connectionSettings");
                    if (node != null)
                    {
                        string dataSource = node.SelectSingleNode("dataSource").InnerText;
                        string port = node.SelectSingleNode("port").InnerText;
                        string username = node.SelectSingleNode("username").InnerText;
                        string password = node.SelectSingleNode("password").InnerText;
                        string database = node.SelectSingleNode("database").InnerText;

                        connectionString = "Server=" + dataSource + ";Port=" + port + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password;
                    }
                    else
                    {
                        MessageBox.Show("As configurações de conexão não foram encontradas no arquivo XML.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar as configurações de conexão: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Arquivo de configuração não encontrado.");
            }
        }

        private void MonitorStatus()
        {
            Thread monitorThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        DataTable dataTable = GetMySQLData();
                        UpdateStatusLabel(dataTable);
                        UpdateDataGridView(dataTable);
                        VerificarStatusEmpresas(); // Adicionamos a verificação do status das empresas aqui
                        AtualizarLabelsEmLoop();
                        dataGridView.CellFormatting += DataGridView_CellFormatting;
                        AtualizarDataGridViewErrosEmLoop();
                    }
                    catch (Exception ex)
                    {
                        if (!errorDisplayed)
                        {
                            MessageBox.Show("Erro ao ler os dados do MySQL: " + ex.Message);
                            errorDisplayed = true;
                        }

                        // Encerra a aplicação completamente
                        Application.Exit();
                    }

                    Thread.Sleep(5000); // Aguarda 5 segundos antes de verificar novamente
                }
            });

            monitorThread.IsBackground = true; 
            monitorThread.Start();
        }

        private DataTable GetMySQLData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Empresa, HoraAtualizacao AS 'Atualização', StatusIS AS '.IS', StatusDG AS 'Gateway', StatusDW AS 'Log do D.W', HoraEvento AS 'Hora Log' FROM status";

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
        }

        private DataTable GetErrorData()
        {
            DataTable errorTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT HoraAtualizacao AS 'Atualização', Empresa, StatusErroDW AS 'Log Erro', HoraEvento AS 'Horário Erro' FROM statuserros";

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    errorTable.Load(reader);
                }
            }

            return errorTable;
        }

        private void UpdateStatusLabel(DataTable dataTable)
        {
            if (lblStatusAtivoPM.InvokeRequired)
            {
                lblStatusAtivoPM.Invoke((MethodInvoker)delegate { UpdateStatusLabel(dataTable); });
            }
            else
            {
                if (dataTable.Rows.Count == 0)
                {
                    lblStatusAtivoPM.Text = "ATUALIZANDO...";
                }
                else
                {
                    lblStatusAtivoPM.Text = "DADOS ATUALIZADOS - Última atualização: " + DateTime.Now.ToString("HH:mm:ss");
                }
            }
        }

        private void UpdateDataGridView(DataTable dataTable)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke((MethodInvoker)delegate { UpdateDataGridView(dataTable); });
            }
            else
            {
                bool notificationDisplayed = false; // Variável para controlar se a notificação já foi exibida

                // Atualizar o DataGridView
                dataGridView.DataSource = dataTable;

                // Loop pelas linhas do DataGridView para colorir de acordo com o status das empresas
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    string statusIS = row.Cells[".IS"].Value.ToString();
                    string Gateway = row.Cells["Gateway"].Value.ToString();

                    // Colorir a célula de verde se estiver em excelente estado, vermelho se houver problemas
                    if (statusIS == "EM EXECUÇÃO" && Gateway == "EM EXECUÇÃO")
                    {
                        row.Cells[".IS"].Style.BackColor = Color.LightGreen;
                        row.Cells["Gateway"].Style.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.Cells[".IS"].Style.BackColor = Color.Red;
                        row.Cells["Gateway"].Style.BackColor = Color.Red;

                        // Verificar se a notificação já foi exibida
                        if (!notificationDisplayed)
                        {
                            // Determinar qual serviço está parado e ajustar a mensagem da notificação
                            string serviceName = "";
                            if (statusIS != "EM EXECUÇÃO")
                            {
                                serviceName = "Ativo .IS";
                            }
                            else if (Gateway != "EM EXECUÇÃO")
                            {
                                serviceName = "Data Gateway";
                            }

                            // Exibir a notificação do Windows com a mensagem apropriada
                            if (!string.IsNullOrEmpty(serviceName))
                            {
                                NotificarServicoParado(serviceName + " parado", row.Cells["Empresa"].Value.ToString());
                                notificationDisplayed = true; // Atualizar a flag para indicar que a notificação foi exibida
                            }
                        }
                    }
                }
            }
        }

        private void UpdateErrorDataGridView(DataTable errorTable)
        {
            if (dataGridViewErros.InvokeRequired)
            {
                dataGridViewErros.Invoke((MethodInvoker)delegate { UpdateErrorDataGridView(errorTable); });
            }
            else
            {
                // Atualizar o DataGridViewErros
                dataGridViewErros.DataSource = errorTable;
            }
        }

        private void AtualizarDataGridViewErrosEmLoop()
        {
            Thread errorUpdateThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        DataTable errorTable = GetErrorData();
                        UpdateErrorDataGridView(errorTable);
                    }
                    catch (Exception ex)
                    {
                        if (!errorDisplayed)
                        {
                            MessageBox.Show("Erro ao atualizar os dados de erro: " + ex.Message);
                            errorDisplayed = true;
                        }
                    }

                    Thread.Sleep(5000); // Aguarda 5 segundos antes de verificar novamente
                }
            });

            errorUpdateThread.IsBackground = true; // Define a thread como background para encerrar junto com a aplicação
            errorUpdateThread.Start();
        }

        private void NotificarServicoParado(string tipoServico, string empresa)
        {
            // Ajustando a mensagem para refletir o tipo de serviço
            notifyIcon1.ShowBalloonTip(5000, "Ativo .PM", "" + tipoServico + " na Empresa " + empresa + "",
                ToolTipIcon.Warning);
        }

        private void buttonSuporte_Click(object sender, EventArgs e)
        {
            if (txtBoxSupPss.Text == "devjoao")
            {
                System.Diagnostics.Process.Start("https://wa.me/xxxxxxxxxxxx");
            }
            else
            {
                MessageBox.Show("Identificador incorreto.");
            }
        }

        private void panelCabecalho_MouseMove(object sender, MouseEventArgs e)
        {
            Move_Form(Handle, e);
        }

        public void Move_Form(IntPtr Handle, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void VerificarStatusEmpresas()
        {
            if (lblEmpSaudaveis.InvokeRequired || lblAmbientesProblemas.InvokeRequired)
            {
                lblEmpSaudaveis.Invoke((MethodInvoker)delegate { VerificarStatusEmpresas(); });
                lblAmbientesProblemas.Invoke((MethodInvoker)delegate { VerificarStatusEmpresas(); });
            }
            else
            {
                int empresasSaudaveis = 0;
                int empresasComProblemas = 0;

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    string statusIS = row.Cells[".IS"].Value.ToString();
                    string Gateway = row.Cells["Gateway"].Value.ToString();

                    if (statusIS == "EM EXECUÇÃO" && Gateway == "EM EXECUÇÃO")
                    {
                        empresasSaudaveis++;
                    }
                    else
                    {
                        empresasComProblemas++;
                    }
                }

                // Atualize os controles da UI dentro deste bloco
                lblEmpSaudaveis.Text = empresasSaudaveis.ToString() + " em excelente estado";
                lblEmpSaudaveis.Font = new Font(lblEmpSaudaveis.Font, FontStyle.Bold);
                lblEmpSaudaveis.ForeColor = Color.DarkOliveGreen;

                lblAmbientesProblemas.Text = empresasComProblemas.ToString() + " com problemas";
                lblAmbientesProblemas.Visible = empresasComProblemas > 0;
            }
        }


        private void button_Minimize_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.Hide();
                this.Visible = false;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.Show();
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
