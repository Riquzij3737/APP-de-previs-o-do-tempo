using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Drawing;  // Para manipula��o de imagens

namespace APDT
{
    public partial class Form1 : Form
    {
        private static readonly string apiKey = "c0243002a578fe19c8a3b05d6b816c7b"; // Substitua com sua chave de API
        private static readonly string apiUrl = "http://api.openweathermap.org/data/2.5/weather";

        private string city;
        private static readonly HttpClient client = new HttpClient();  // Inst�ncia reutiliz�vel do HttpClient

        public Form1(string city)
        {
            InitializeComponent();
            this.city = city;
            Dados(); // Chama o m�todo de dados para preencher as informa��es ao iniciar o formul�rio
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // C�digo n�o est� sendo utilizado, voc� pode adicionar algo se necess�rio
        }

        private async void Dados()
        {
            var weatherData = await GetWeatherDataAsync(city);

            if (weatherData != null)
            {
                // Atualiza os labels com as informa��es de clima
                this.Text = $"Previs�o do tempo de {weatherData.Name}";
                label6.Text = $"{weatherData.Main.Temp} �C";
                label2.Text = $"Temp.Min: {weatherData.Main.TempMin} �C";
                label3.Text = $"Temp.Max: {weatherData.Main.TempMax} �C";

                // Verifica se a descri��o do clima existe antes de acessar
                if (weatherData.Weather != null && weatherData.Weather.Length > 0)
                {
                    label4.Text = $"{weatherData.Weather[0].Description}";

                    // Verifica a descri��o do clima e realiza uma a��o com base nela
                    string description = weatherData.Weather[0].Description.ToLower();
                    Console.WriteLine($"Clima atual em {city}: {description}");                    

                    try
                    {
                        if (description.Contains("clear"))
                        {
                            pictureBox1.Image = Properties.Resources.SolLimpo;
                        }
                        else if (description.Contains("rain"))
                        {
                            pictureBox1.Image = Properties.Resources.Chuva;
                        }
                        else if (description.Contains("snow"))
                        {
                            pictureBox1.Image = Properties.Resources.Neve;
                        }
                        else if (description.Contains("cloud"))
                        {
                            pictureBox1.Image = Properties.Resources.Nublado;
                        }
                        else
                        {
                            Console.WriteLine("O clima est� imprevis�vel! Fique atento �s mudan�as.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao carregar imagem: {ex.Message}");
                    }

                }
            }
            else
            {
                Console.WriteLine("N�o foi poss�vel obter as informa��es do clima.");
            }
        }

        // M�todo ass�ncrono para buscar dados de clima da API
        public async Task<WeatherResponse> GetWeatherDataAsync(string city)
        {
            try
            {
                string url = $"{apiUrl}?q={city}&appid={apiKey}&units=metric&lang=pt_br";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherResponse>(content);
                }
                else
                {
                    Console.WriteLine($"Erro ao acessar a API: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
        }
    }

    // Classes para deserializar a resposta da API
    public class WeatherResponse
    {
        public string Name { get; set; }
        public MainWeather Main { get; set; }
        public Weather[] Weather { get; set; }
    }

    public class MainWeather
    {
        public float Temp { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }
}
