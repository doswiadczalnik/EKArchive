using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EKArchive
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void FetchDataButton_Click(object sender, EventArgs e)
        {
            string selectedDate = DatePicker.Value.ToString("yyyy-MM-dd");
            string nextDate = DatePicker.Value.AddDays(1).ToString("yyyy-MM-dd");

            string url = $"https://api.raporty.pse.pl/api/pdgsz?$filter=udtczas ge '{selectedDate}' and udtczas lt '{nextDate}'";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(url);

                    var data = JsonConvert.DeserializeObject<ApiResponse>(response);

                    DataGrid.Rows.Clear();

                    if (data?.Value == null || data.Value.Count == 0)
                    {
                        BusinessDateLabel.Text = $"Doba handlowa: {selectedDate}";
                        SourceDateLabel.Text = $"Data publikacji: brak danych";
                        MessageBox.Show("Brak danych dla wybranej daty.");

                        return;
                    }

                    string businessDate = data.Value[0].business_date ?? "Brak danych";
                    string sourceDatetime = data.Value[0].source_datetime?.Split('.')[0] ?? "Brak danych";

                    BusinessDateLabel.Text = $"Doba handlowa: {businessDate}";
                    SourceDateLabel.Text = $"Data publikacji: {sourceDatetime}";

                    foreach (var item in data.Value)
                    {
                        string time = item.UdtCzas?.Split(' ')[1] ?? "Brak danych";
                        int alert = item.Znacznik;

                        int rowIndex = DataGrid.Rows.Add(time, GetAlertText(alert));

                        var row = DataGrid.Rows[rowIndex];
                        ApplyRowColor(row, alert);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pobierania danych: {ex.Message}");
            }
        }

        private static void ApplyRowColor(DataGridViewRow row, int alert)
        {
            switch (alert)
            {
                case 0: // Zalecane użytkowanie
                    row.DefaultCellStyle.BackColor = Color.FromArgb(34, 107, 17);
                    row.DefaultCellStyle.ForeColor = Color.White;
                    break;
                case 1: // Normalne użytkowanie
                    row.DefaultCellStyle.BackColor = Color.FromArgb(152, 194, 29);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;
                case 2: // Zalecane oszczędzanie
                    row.DefaultCellStyle.BackColor = Color.FromArgb(242, 196, 51);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;
                case 3: // Wymagane ograniczanie
                    row.DefaultCellStyle.BackColor = Color.FromArgb(228, 35, 19);
                    row.DefaultCellStyle.ForeColor = Color.White;
                    break;
                default: // Domyślny styl
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }
        }

        private static string GetAlertText(int alert)
        {
            return alert switch
            {
                0 => "ZALECANE UŻYTKOWANIE",
                1 => "NORMALNE UŻYTKOWANIE",
                2 => "ZALECANE OSZCZĘDZANIE",
                3 => "WYMAGANE OGRANICZANIE",
                _ => "Nieznany znacznik"
            };
        }
    }
}
