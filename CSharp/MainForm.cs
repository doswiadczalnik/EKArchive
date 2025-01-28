using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EKArchive
{
    public partial class MainForm : Form
    {
        private readonly Lazy<CacheManager> _cacheManager = new Lazy<CacheManager>(() => new CacheManager());


        public MainForm()
        {
            InitializeComponent();
        }

        private async void FetchDataButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DatePicker.Value.Date;
            DateTime nextDate = selectedDate.AddDays(1);
            DateTime today = DateTime.Now.Date;

            List<ApiData> data = null;
            if (selectedDate != today)
            {
                data = _cacheManager.Value.GetFromCache(selectedDate);
            }

            if (data == null)
            {
                data = await FetchDataFromApi(selectedDate, nextDate);

                if (selectedDate != today && data != null)
                {
                    _cacheManager.Value.SaveToCache(selectedDate, data);
                }
            }

            if (data == null || data.Count <= 0)
            {
                MessageBox.Show("Brak danych dla wybranej daty.", "Informacja", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            DisplayData(data);
        }

        private static async Task<List<ApiData>> FetchDataFromApi(DateTime selectedDate, DateTime nextDate)
        {
            string url = $"https://api.raporty.pse.pl/api/pdgsz?$filter=udtczas ge '{selectedDate:yyyy-MM-dd}' and udtczas lt '{nextDate:yyyy-MM-dd}'";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string response = await client.GetStringAsync(url);

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
                    return apiResponse?.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Błąd podczas pobierania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void DisplayData(List<ApiData> data)
        {
            string businessDateText, sourceDateText;

            DataGrid.Rows.Clear();

            if (data != null && data.Count > 0)
            {
                businessDateText = $"{data[0].BusinessDate:yyyy-MM-dd}";
                sourceDateText = $"{data[0].SourceDatetime:yyyy-MM-dd HH:mm:ss}";

                foreach (var item in data)
                {
                    string time = item.UdtCzas.ToString("HH:mm");
                    int alert = item.Znacznik;

                    int rowIndex = DataGrid.Rows.Add(time, GetAlertText(alert));

                    var row = DataGrid.Rows[rowIndex];
                    ApplyRowColor(row, alert);
                }
            }
            else
            {
                businessDateText = sourceDateText = "Brak danych";
            }

            BusinessDateLabel.Text = $"Doba handlowa: {businessDateText}";
            SourceDateLabel.Text = $"Data publikacji: {sourceDateText}";
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
