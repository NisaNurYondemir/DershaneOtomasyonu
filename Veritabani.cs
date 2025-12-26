using System;
using System.Data;
using System.Windows.Forms;
using Npgsql; // İndirdiğimiz kütüphaneyi ekliyoruz

namespace DershaneOtomasyonu // Projenizin adı neyse o kalacak
{
    public static class Veritabani
    {
        private static string baglantiAdresi = "Host=localhost;Port=5432;Database=DershaneOtomasyonu2;Username=postgres;Password=12345";

        public static NpgsqlConnection BaglantiGetir()
        {
            var baglanti = new NpgsqlConnection(baglantiAdresi);
            // AÇMA İŞLEMİNİ ÇAĞIRAN YAPSIN
            return baglanti;
        }
    }
}