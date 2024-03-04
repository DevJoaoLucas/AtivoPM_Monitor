using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AtivoPM
{
    public partial class FormMonitor : Form
    {
        private string connectionString = "Server=localhost;Port=3306;Database=ativopm;Uid=ativopm;Pwd=;";
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public FormMonitor()
        {
            InitializeComponent();
            MonitorStatus();
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
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Erro ao ler os dados do MySQL: {e.Message}");
                    }

                    Thread.Sleep(10000); // 10 segundos
                }
            });

            monitorThread.Start();
        }

        private DataTable GetMySQLData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Empresa, Servico, Status, Hora FROM status";

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable;
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
                dataGridView.DataSource = dataTable;
            }
        }

        private void buttonSuporte_Click(object sender, EventArgs e)
        {
            {
                if (txtBoxSupPss.Text == "joaodev")
                {
                    System.Diagnostics.Process.Start("https://wa.me/5592988225112");
                }
                else
                {
                    MessageBox.Show("Identificador incorreto.");
                }
            }
        }

       private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
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
    }
}