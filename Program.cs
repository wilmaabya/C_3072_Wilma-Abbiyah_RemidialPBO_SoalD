using System;

namespace BankPelita
{
    class BankAccount
    {
        public string NomorRekening { get; }
        public string NamaPemilik { get; }

        private decimal saldo;

        public BankAccount(string nomorRekening, string namaPemilik, decimal saldoAwal = 0m)
        {
            NomorRekening = nomorRekening;
            NamaPemilik = namaPemilik;
            saldo = saldoAwal;
        }

        public void Setor(decimal jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah tidak valid");
                return;
            }
            saldo += jumlah;
            Console.WriteLine($"Setor Rp {jumlah:N0} sukses. Saldo sekarang: Rp {saldo:N0}");
        }

        public void Tarik(decimal jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah tidak valid");
                return;
            }
            if (jumlah > saldo)
            {
                Console.WriteLine("Saldo tidak mencukupi");
                return;
            }
            saldo -= jumlah;
            Console.WriteLine($"Tarik Rp {jumlah:N0} sukses. Saldo sekarang: Rp {saldo:N0}");
        }

        public void Transfer(BankAccount tujuan, decimal jumlah)
        {
            if (tujuan == null)
            {
                Console.WriteLine("Rekening tujuan tidak ada");
                return;
            }
            if (jumlah <= 0)
            {
                Console.WriteLine("Jumlah tidak valid");
                return;
            }
            if (jumlah > saldo)
            {
                Console.WriteLine("Saldo tidak mencukupi");
                return;
            }

            saldo -= jumlah;
            tujuan.saldo += jumlah;
            Console.WriteLine($"Transfer Rp {jumlah:N0} ke {tujuan.NamaPemilik} berhasil");
            Console.WriteLine($"   Saldo Anda sekarang: Rp {saldo:N0}");
        }

        public void TampilkanInfo()
        {
            Console.WriteLine($"No. Rekening : {NomorRekening}");
            Console.WriteLine($"Nama Pemilik : {NamaPemilik}");
            Console.WriteLine($"Saldo        : Rp {saldo:N0}");
        }
    }

    class Bank
    {
        private BankAccount[] akun = new BankAccount[10];
        private int jumlahNasabah = 0;

        public void TambahSampleData()
        {
            akun[0] = new BankAccount("001", "Lando", 5_000_000);
            akun[1] = new BankAccount("002", "Charles", 3_000_000);
            akun[2] = new BankAccount("003", "George", 7_500_000);
            jumlahNasabah = 3;
        }

        public BankAccount Cari(string nomorRekening)
        {
            for (int i = 0; i < jumlahNasabah; i++)
            {
                if (akun[i].NomorRekening == nomorRekening)
                    return akun[i];
            }
            return null;
        }

        public void TampilkanSemua()
        {
            Console.WriteLine("\nDAFTAR REKENING NASABAH");
            for (int i = 0; i < jumlahNasabah; i++)
            {
                akun[i].TampilkanInfo();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Bank bank = new Bank();
            bank.TambahSampleData();

            while (true)
            {
                Console.WriteLine("\n=== SISTEM DIGITAL BANK PELITA ===");
                Console.WriteLine("1. Lihat Data Rekening");
                Console.WriteLine("2. Setor Tunai");
                Console.WriteLine("3. Tarik Tunai");
                Console.WriteLine("4. Transfer Antar Rekening");
                Console.Write("Pilih menu (1-4): ");
                string pilih = Console.ReadLine();

                switch (pilih)
                {
                    case "1":
                        bank.TampilkanSemua();
                        break;
                    case "2":
                        Setor(bank);
                        break;
                    case "3":
                        Tarik(bank);
                        break;
                    case "4":
                        Transfer(bank);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid");
                        break;
                }
            }
        }

        static void Setor(Bank bank)
        {
            var acc = MintaRekening(bank, "Nomor Rekening: ");
            if (acc == null) return;

            Console.Write("Jumlah setor: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal jml))
                acc.Setor(jml);
            else
                Console.WriteLine("Input tidak valid.");
        }

        static void Tarik(Bank bank)
        {
            var acc = MintaRekening(bank, "Nomor Rekening: ");
            if (acc == null) return;

            Console.Write("Jumlah tarik: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal jml))
                acc.Tarik(jml);
            else
                Console.WriteLine("Input tidak valid.");
        }

        static void Transfer(Bank bank)
        {
            var sumber = MintaRekening(bank, "Nomor Rekening Anda  : ");
            if (sumber == null) return;

            var tujuan = MintaRekening(bank, "Nomor Rekening Tujuan: ");
            if (tujuan == null) return;

            Console.Write("Jumlah transfer: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal jml))
                sumber.Transfer(tujuan, jml);
            else
                Console.WriteLine("Input tidak valid");
        }

        static BankAccount MintaRekening(Bank bank, string pesan)
        {
            Console.Write(pesan);
            string no = Console.ReadLine();
            var acc = bank.Cari(no);
            if (acc == null)
                Console.WriteLine("Rekening tidak ada");
            return acc;
        }
    }
}
