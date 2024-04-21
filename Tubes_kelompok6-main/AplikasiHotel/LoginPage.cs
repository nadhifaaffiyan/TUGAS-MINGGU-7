using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AplikasiHotel
{
    public partial class LoginPage : Form
    {
        // Penamaan Underscore Prefix untuk variabel private
        private Config _config;
        private string _path;
        private string _configFileName;
        public LoginPage()
        {
            InitializeComponent();
            _path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            _configFileName = "Login_config.json";

            try
            {
                ReadConfig();
            }
            catch
            {
                SetDefault();
                WriteConfig();
            }
        }

        // Penamaan Pascal Case untuk method 'ReadConfig'
        private void ReadConfig()
        {
            string jsonFromFile = File.ReadAllText(Path.Combine(_path, _configFileName));
            _config = JsonSerializer.Deserialize<Config>(jsonFromFile);
        }

        // Penamaan Pascal Case untuk method 'WriteConfig'
        private void WriteConfig()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(_config, options);
            string fullPath = Path.Combine(_path, _configFileName);
            File.WriteAllText(fullPath, jsonString);
        }

        // Penamaan Pascal Case untuk method 'SetDefault'
        private void SetDefault()
        {
            _config = new Config("admin", "password123", "Login sukses", "Login gagal");
        }
        public class Config
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Login_sukses { get; set; }
            public string Login_gagal { get; set; }

            public Config() { }

            public Config(string Username, string Password, string Login_sukses, string Login_gagal)
            {
                this.Username = Username;
                this.Password = Password;
                this.Login_sukses = Login_sukses;
                this.Login_gagal = Login_gagal;
            }
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Trim() digunakan untuk menghapus spasi ekstra di awal dan akhir string 
             * untuk membantu menghindari validasi yang tidak valid karena spasi tambahan.
            */
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Input tidak boleh kosong");
                return;
            }

            if (_config.Username == username && _config.Password == password)
            {
                Dashboard ds = new Dashboard();
                ds.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(_config.Login_gagal);
            }
        }

        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
