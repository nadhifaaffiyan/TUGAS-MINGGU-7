using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace AplikasiHotel
{
    public partial class Dashboard : Form
    {
        public int totalAmount = 0;
        // Menggunakan List untuk menggantikan array dan menghindari deklarasi ukuran
        List<int> noKmr1 = new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 109 };
        List<int> noKmr2 = new List<int>() { 201, 202, 203, 204, 205, 206, 207, 208, 209 };
        List<int> noKmr3 = new List<int>() { 301, 302, 303, 304, 305, 306, 307, 308, 309 };
        List<string> status1 = new List<string>() { "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia" };
        List<string> status2 = new List<string>() { "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia" };
        List<string> status3 = new List<string>() { "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia", "tersedia" };
        List<Pesanan> daftarPesanan = new List<Pesanan>();

        int jmlKmr1 = 9;
        int jmlKmr2 = 9;
        int jmlKmr3 = 9;


        public class Pesanan
        {
            public string Nama { get; set; }
            public string Email { get; set; }
            public string JenisKamar { get; set; }
            public int LamaMenginap { get; set; }
            public int TotalAmount { get; set; }
        }

        private Pesanan pesanan;




        Dictionary<int, string> menuMakanan = new Dictionary<int, string>()
        {
            { 1, "Nasi Goreng" },
            { 2, "Mie Goreng" },
            { 3, "Ayam Bakar" },
            { 4, "Sate Ayam" }
        };
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //FITUR PESAN MAKAN

        //Menampilkan Menu Yang Tersedia 
        private void label21_Click(object sender, EventArgs e)
        {
            string menuText = "Menu Makanan Tersedia:\n";

            foreach (var menuItem in menuMakanan)
            {
                menuText += $"{menuItem.Key}. {SanitizeInput(menuItem.Value)}\n";
            }

            label2.Text = menuText;
        }

        //button untuk melakukan pemesanan 
        private void btnPesan_Click(object sender, EventArgs e)
        {
            int nomorMakanan;
            if (int.TryParse(SanitizeInput(inputmakanan.Text), out nomorMakanan))
            {
                if (menuMakanan.ContainsKey(nomorMakanan))
                {
                    string makanan = menuMakanan[nomorMakanan];
                    statusPemesanan.Text = "Pemesanan berhasil: " + SanitizeInput(makanan);
                }
                else
                {
                    statusPemesanan.Text = "Nomor makanan tidak valid";
                }
            }
            else
            {
                statusPemesanan.Text = "Nomor makanan harus berupa angka";
            }
        }

        private string SanitizeInput(string input)
        {
            // Menghilangkan karakter yang tidak diinginkan atau berpotensi membahayakan
            return input.Trim();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void Pemesanan_Click(object sender, EventArgs e)
        {

        }

        private void inputmakanan_TextChanged(object sender, EventArgs e)
        {

        }

        //FITUR PESAN KAMAR
        private void jenisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        // Dictionary untuk memetakan jenis kamar ke harga per malam
        Dictionary<string, int>  jenisKamar = new Dictionary<string, int>()
        {
            { "Single", 750000 },
            { "Double", 1000000 },
            { "Suite", 1500000 }
        };


        // Method untuk memperbarui total jumlah yang harus dibayarkan
        private void UpdateTotalAmount()
        {
            int hargaPerMalam = 0;
            int totalAmount = 0;
            int lamaMenginap = Convert.ToInt32(hariNumericUpDown.Value);
            string selectedJenis = jenisComboBox.SelectedItem.ToString();

            if (jenisKamar.ContainsKey(selectedJenis))
            {
                hargaPerMalam = jenisKamar[selectedJenis];
                totalAmount = hargaPerMalam * lamaMenginap;
            }
            else
            {
                hargaPerMalam = 0;
                totalAmount = 0;
            }

            totalLabel.Text = totalAmount.ToString();
        }

        private bool isDataValid = false; // Flag untuk menandakan validitas data
        private void nextButton_Click(object sender, EventArgs e)
        {
            // Memeriksa apakah nama dan email telah diisi
            if (string.IsNullOrEmpty(namaTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text))
            {
                MessageBox.Show("Nama dan Email harus diisi.");
                isDataValid = false; // Tandai data tidak valid
                return;
            }

            // Memeriksa apakah jenis kamar telah dipilih
            if (jenisComboBox.SelectedItem == null)
            {
                MessageBox.Show("Jenis kamar belum dipilih.");
                isDataValid = false; // Tandai data tidak valid
                return;
            }

            // Memeriksa validitas format email 
            string emailInput = emailTextBox.Text;
            if (!IsValidEmail(emailInput))
            {
                MessageBox.Show("Format email tidak valid.");
                return;
            }

            // Memeriksa lama menginap
            int lamaMenginap = Convert.ToInt32(hariNumericUpDown.Value);
            if (lamaMenginap == 0)
            {
                MessageBox.Show("Hari belum dipilih.");
                isDataValid = false; // Tandai data tidak valid
                return;
            }

            UpdateTotalAmount(); // memanggil fungsi UpdateReviewLabel untuk ditampilkan di label
           
            isDataValid = true; // Set flag menjadi true jika semua kondisi terpenuhi
        }

        private void totalLabel_Click(object sender, EventArgs e)
        {

        }

        private void pesanButton_Click(object sender, EventArgs e)
        {
            // Memeriksa validitas data
            if (!isDataValid)
            {
                MessageBox.Show("Pesanan tidak valid. Lakukan pengecekan terlebih dahulu.");
                return;
            }

            // Memeriksa apakah nama dan email telah diisi
            if (string.IsNullOrEmpty(namaTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text))
            {
                isDataValid = false; // Tandai data tidak valid
                return;
            }

            

            // Memeriksa lama menginap
            int lamaMenginap = Convert.ToInt32(hariNumericUpDown.Value);
            if (lamaMenginap == 0)
            {
                isDataValid = false; // Tandai data tidak valid
                return;
            }

            UpdateTotalAmount();

            string selectedJenis = jenisComboBox.SelectedItem.ToString();
            bool roomFound = false;
            List<int> nomorKamar = null;
            List<string> statusKamar = null;

            // Memproses pemesanan berdasarkan tipe kamar yang dipilih
            switch (selectedJenis)
            {
                case "Single":
                    nomorKamar = noKmr1;
                    statusKamar = status1;
                    break;
                case "Double":
                    nomorKamar = noKmr2;
                    statusKamar = status2;
                    break;
                case "Suite":
                    nomorKamar = noKmr3;
                    statusKamar = status3;
                    break;
                default:
                    MessageBox.Show("Tipe kamar tidak valid.");
                    isDataValid = false; // Tandai data tidak valid
                    return;
            }

            // Pemesanan kamar
            for (int i = 0; i < nomorKamar.Count; i++)
            {
                if (statusKamar[i] == "tersedia")
                {
                    statusKamar[i] = "terisi";
                    MessageBox.Show("Pesanan kamar " + selectedJenis + " dengan nomor " + nomorKamar[i] + " berhasil.");
                    roomFound = true;
                    SavePesananData(); // Panggil fungsi untuk menyimpan data pesanan
                    break;
                }
            }

            // Memeriksa apakah kamar tersedia
            if (!roomFound)
            {
                MessageBox.Show("Kamar sudah penuh.");
                isDataValid = false; // Tandai data tidak valid
                return;
            }

            // Menandai data valid setelah pemesanan sukses
            isDataValid = true;

            // Mengosongkan pengisian
            namaTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            jenisComboBox.SelectedItem = null;
            hariNumericUpDown.Value = 0;
        }

        //FITUR CEK HARGA KAMAR (informasi Hotel)
        private void btnCek_Click(object sender, EventArgs e)
        {
            if (comboBoxJenisKamar.SelectedItem.Equals("Single"))
            {
                labelInformasiHarga.Text = "750000";
            }
            else if (comboBoxJenisKamar.SelectedItem.Equals("Double"))
            {
                labelInformasiHarga.Text = "1000000";
            }
            else if (comboBoxJenisKamar.SelectedItem.Equals("Suite"))
            {
                labelInformasiHarga.Text = "1500000";
            }
        }
        //FITUR CEK KETERSEDIAN KAMAR
        private void cekKmrButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Mengambil nomor kamar dari input teks
                int nomorKamar = int.Parse(cekKmrTextBox.Text);

                string statusKamar = string.Empty;

                // Memeriksa nomor kamar pada setiap tipe kamar
                if (noKmr1.Contains(nomorKamar))
                {
                    // Mengambil indeks kamar pada tipe kamar 1
                    int index = noKmr1.IndexOf(nomorKamar);
                    // Mendapatkan status kamar
                    statusKamar = status1[index];
                }
                else if (noKmr2.Contains(nomorKamar))
                {
                    // Mengambil indeks kamar pada tipe kamar 2
                    int index = noKmr2.IndexOf(nomorKamar);
                    // Mendapatkan status kamar
                    statusKamar = status2[index];
                }
                else if (noKmr3.Contains(nomorKamar))
                {
                    // Mengambil indeks kamar pada tipe kamar 3
                    int index = noKmr3.IndexOf(nomorKamar);
                    // Mendapatkan status kamar
                    statusKamar = status3[index];
                }
                else
                {
                    // Jika nomor kamar tidak ditemukan
                    statusKmrLabel.Text = "No kamar tidak ada";
                    return;
                }

                // Menampilkan status kamar berdasarkan nilai statusKamar
                if (statusKamar == "tersedia")
                {
                    statusKmrLabel.Text = "Kamar Tersedia";
                }
                else if (statusKamar == "terisi")
                {
                    statusKmrLabel.Text = "Kamar Terisi";
                }
            }
            catch (Exception ex)
            {
                // Jika input tidak valid (bukan angka)
                statusKmrLabel.Text = "Input Angka!";
            }
        }


        private void statusKmrLabel_Click(object sender, EventArgs e)
        {

        }

        private void cekKmrTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        // Logout
        private void logOutButton_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Hide();
        }
        //CheckOut
        private void checkOutButton_Click(object sender, EventArgs e) { 
            string namaPemesan = namaCheckOutTextBox.Text;
            int nomorKamar;

            if (int.TryParse(noKamarCheckOutTextBox.Text, out nomorKamar))
            {
                if (IsSingleRoomNumber(nomorKamar))
                {
                    int index = nomorKamar - 101;
                    if (status1[index] == "terisi")
                    {
                        status1[index] = "tersedia";
                        MessageBox.Show($"Check out berhasil untuk kamar nomor {nomorKamar}");
                    }
                    else
                    {
                        MessageBox.Show($"Kamar dengan nomor {nomorKamar} belum dipesan");
                    }
                }
                else if (IsDoubleRoomNumber(nomorKamar))
                {
                    int index = nomorKamar - 201;
                    if (status2[index] == "terisi")
                    {
                        status2[index] = "tersedia";
                        MessageBox.Show($"Check out berhasil untuk kamar nomor {nomorKamar}");
                    }
                    else
                    {
                        MessageBox.Show($"Kamar dengan nomor {nomorKamar} belum dipesan");
                    }
                }
                else if (IsSuiteRoomNumber(nomorKamar))
                {
                    int index = nomorKamar - 301;
                    if (status3[index] == "terisi")
                    {
                        status3[index] = "tersedia";
                        MessageBox.Show($"Check out berhasil untuk kamar nomor {nomorKamar}");
                    }
                    else
                    {
                        MessageBox.Show($"Kamar dengan nomor {nomorKamar} belum dipesan");
                    }
                }
                else
                {
                    MessageBox.Show("Nomor kamar tidak valid");
                }
            }
            else
            {
                MessageBox.Show("Nomor kamar harus berupa angka");
            }
        }

        private bool IsSingleRoomNumber(int nomorKamar)
        {
            return nomorKamar >= 101 && nomorKamar <= 109;
        }

        private bool IsDoubleRoomNumber(int nomorKamar)
        {
            return nomorKamar >= 201 && nomorKamar <= 209;
        }

        private bool IsSuiteRoomNumber(int nomorKamar)
        {
            return nomorKamar >= 301 && nomorKamar <= 309;
        }


        private void namaCheckOutTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void noKamarCheckOutTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        // FITUR PELAYANAN
        private void BtnPesanJasa_Click(object sender, EventArgs e)
        {
            string selectedItem = comboBoxJasa.SelectedItem.ToString(); // Mendapatkan item yang dipilih dari ComboBox
            string message = "";

            // Mencari item yang cocok di dalam ListBox
            foreach (string item in listBoxJasa.Items)
            {
                if (item.StartsWith(selectedItem))
                {
                    message = "Anda telah memesan " + selectedItem + ". Terima kasih!";
                    MessageBox.Show(message); // Menampilkan pesan yang cocok dari ListBox
                    break; 
                }
            }
        }
        // Method untuk memvalidasi jika email yang di masukan valid atau tidak
        private bool IsValidEmail(string email)
        {
            try
            {
                // Mengecek jika inputan user sesuai format email yang benar
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void SavePesananData()
        {
            // Memeriksa apakah nama dan email telah diisi(secure code)
            if (string.IsNullOrEmpty(namaTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text))
            {
                MessageBox.Show("Data belum tersedia.");
                return;
            }

            // Mengambil data yang telah dipilih oleh pengguna
            string nama = namaTextBox.Text;
            string email = emailTextBox.Text;
            string jenisKamar = jenisComboBox.SelectedItem?.ToString();
            int lamaMenginap = Convert.ToInt32(hariNumericUpDown.Value);
            int totalHarga = Convert.ToInt32(totalLabel.Text);

            // Membuat objek Pesanan baru
            Pesanan pesananBaru = new Pesanan
            {
                Nama = nama,
                Email = email,
                JenisKamar = jenisKamar,
                LamaMenginap = lamaMenginap,
                TotalAmount = totalHarga
            };

            // Memeriksa apakah data pesanan baru berbeda dengan pesanan sebelumnya
            if (!IsPesananAlreadyExists(pesananBaru))
            {
                // Menambahkan pesanan baru ke daftarPesanan
                daftarPesanan.Add(pesananBaru);

                // Menambahkan data pemesanan ke dataGridView1
                dataGridView1.Rows.Add(pesananBaru.Nama, pesananBaru.Email, pesananBaru.JenisKamar,
                    pesananBaru.TotalAmount, pesananBaru.LamaMenginap);
            }
        }

        // FITUR RIWAYAT RESERVASI
        private void btnRiwayat_Click(object sender, EventArgs e)
        {
            SavePesananData();
        }


        // Method untuk mengecek apakah data reservasi sudah masuk datagrid atau belum 
        // untuk mencegah duplikasi (secure code)
        private bool IsPesananAlreadyExists(Pesanan pesanan)
        {
            foreach (Pesanan pesananExisting in daftarPesanan)
            {
                // perbandingan antara data pesanan baru dengan pesanan yang sudah ada
                if (pesanan.Nama == pesananExisting.Nama &&
                    pesanan.Email == pesananExisting.Email &&
                    pesanan.JenisKamar == pesananExisting.JenisKamar &&
                    pesanan.LamaMenginap == pesananExisting.LamaMenginap &&
                    pesanan.TotalAmount == pesananExisting.TotalAmount)
                {
                    return true; // Pesanan sudah ada dalam daftarPesanan
                }
            }
            return false; // Pesanan belum ada dalam daftarPesanan

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        HotelReviewSystem<int> hotelReviews = new HotelReviewSystem<int>();

        private void SubmitReviewButton_Click(object sender, EventArgs e)
        {
            int roomNumber;
            string submitUlasan = roomNumberTextBox.Text;

            bool isValidRoomNumber = int.TryParse(submitUlasan, out roomNumber) &&
                ((roomNumber >= 101 && roomNumber <= 109) ||
                 (roomNumber >= 201 && roomNumber <= 209) ||
                 (roomNumber >= 301 && roomNumber <= 309));

            if (!isValidRoomNumber)
            {
                ShowErrorMessage("Nomor Kamar tidak valid. Mohon masukkan angka antara 101 - 109, 201 - 209, 301 - 309.");
                return;
            }

            int rating;
            string ratingInput = ratingTextBox.Text;

            bool isValidRating = int.TryParse(ratingInput, out rating) && rating >= 1 && rating <= 5;

            if (!isValidRating)
            {
                ShowErrorMessage("Rating tidak valid. Mohon masukkan angka antara 1 hingga 5.");
                return;
            }

            string comment = commentTextBox.Text;

            ReviewKamar<int> review = new ReviewKamar<int>
            {
                RoomNumber = roomNumber,
                Rating = rating,
                Comment = comment
            };

            hotelReviews.AddReview(review);
            ShowSuccessMessage("Ulasan berhasil ditambahkan.");

            UpdateReviewLabel(); //update label pada fitur lihat ulasan

            ClearInputFields(); // mengosongkan inputan
        }

        private void UpdateReviewLabel()
        {
            StringBuilder sb = new StringBuilder();

            List<ReviewKamar<int>> allReviews = hotelReviews.GetReviews();

            sb.AppendLine("-- Ulasan Kamar --");
            sb.AppendLine();

            foreach (ReviewKamar<int> review in allReviews)
            {
                sb.AppendFormat("Kamar {0}:\n", review.RoomNumber);
                sb.AppendFormat("Rating: {0}/5\n", review.Rating);
                sb.AppendFormat("Komentar: {0}\n\n", review.Comment);
            }

            reviewLabel.Text = sb.ToString(); // mengupdate teks yang ditampilkan pada kontrol
        }

        private void reviewLabel_Click(object sender, EventArgs e)
        {
            UpdateReviewLabel(); // memanggil fungsi UpdateReviewLabel untuk ditampilkan di label
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearInputFields()
        {
            roomNumberTextBox.Text = string.Empty;
            ratingTextBox.Text = string.Empty;
            commentTextBox.Text = string.Empty;
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void informasiTab_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void pesanKmrTab_Click(object sender, EventArgs e)
        {

        }
    }
}
