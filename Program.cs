using System;
using System.Collections.Generic;

namespace komisi_plus_gajipokok_pegawai_abstractclass
{
    public abstract class Pegawai
    {
        public string NamaDepan { get; }
        public string NamaBelakang { get; }
        public string NomerKTP { get; }

        public Pegawai(string namaDepan, string namaBelakang, string nomerKTP)
        {
            NamaDepan = namaDepan;
            NamaBelakang = namaBelakang;
            NomerKTP = nomerKTP;
        }
        public override string ToString() => $"{NamaDepan} {NamaBelakang}\n" + $"Nomer KTP = {NomerKTP}";
        public abstract decimal Pendapatan();
    }
    public class GajiPegawai : Pegawai
    {
        private decimal gajiMingguan;
        public GajiPegawai(string namaDepan, string namaBelakang, string nomerKTP, decimal gajiMingguan) : base(namaDepan,namaBelakang,nomerKTP)
        {
            GajiMingguan = gajiMingguan;
        }
        public decimal GajiMingguan
        {
            get
            {
                return gajiMingguan;
            }
            set
            {
                if (value<0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value,$"{nameof(GajiMingguan)}must be>=0");
                }
                gajiMingguan = value;
            }
        }
        public override decimal Pendapatan() => GajiMingguan;
        public override string ToString() =>
            $"Gaji Pegawai = {base.ToString()}\n" +
            $"Gaji Mingguan = {GajiMingguan:C}\n";
    }
    public class UpahJamPegawai : Pegawai
    {
        private decimal upah;
        private decimal jam;
        public UpahJamPegawai (string namaDepan, string namaBelakang, string nomerKTP, decimal upahPerJam, decimal JamKerja) 
            : base (namaDepan, namaBelakang,nomerKTP)
        {
            upah = upahPerJam;
            jam = JamKerja;
        }
        public decimal Upah
        {
            get
            {
                return upah;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(Upah)}must be>=0");
                }
                upah = value;
            }
        }
        public decimal Jam
        {
            get
            {
                return jam;
            }
            set
            {
                if (value < 0 || value < 168)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value, $"{nameof(Jam)}must be>=0 and <=168");
                }
                jam = value;
            }
        }
        public override decimal Pendapatan()
        {
            if (Jam <=40)
            {
                return Upah * Jam;
            }
            else
            {
                return (40 * Upah) + ((Jam - 40) * Upah * 1.5M);
            }
        }
        public override string ToString() =>
        $"Upah Jam Pegawai = {base.ToString()}\n" +
        $"Upah per Jam = {Upah:C}\n" + $"Jam Kerja Pegawai = {Jam:F2}\n";
    }
    public class KomisiPegawai : Pegawai
    {
        private decimal pendapatanKotor;
        private decimal tarifKomisi;

        public KomisiPegawai(string namaDepan, string namaBelakang, string nomerKTP, decimal pendapatanKotor, decimal tarifKomisi)
            : base(namaDepan, namaBelakang, nomerKTP)
        {
            PendapatanKotor = pendapatanKotor;
            TarifKomisi = tarifKomisi;
        }
        public decimal PendapatanKotor
        {
            get
            {
                return pendapatanKotor;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(PendapatanKotor)} must be >=0");
                }
                pendapatanKotor = value;
            }
        }
        public decimal TarifKomisi
        {
            get
            {
                return tarifKomisi;
            }
            set
            {
                if (value <= 0 || value >= 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(TarifKomisi)} must be >0 and <1");
                }
                tarifKomisi = value;
            }
        }
        public override decimal Pendapatan() => TarifKomisi * PendapatanKotor;
        public override string ToString() =>
            $"Komisi Pegawai = {base.ToString()}\n" +
            $"Pendapatan Kotor = {PendapatanKotor:C}\n" +
            $"Tarif Komisi = {TarifKomisi:F2}\n";
    }
    public class KomisiPlusGajiPokokPegawai : KomisiPegawai
    {
        private decimal gajiPokok;

        public KomisiPlusGajiPokokPegawai(string namaDepan, string namaBelakang, string nomerKTP, decimal pendapatanKotor, decimal tarifKomisi, decimal gajiPokok)
            : base(namaDepan, namaBelakang, nomerKTP, pendapatanKotor, tarifKomisi)
        {
            GajiPokok = gajiPokok;
        }
        public decimal GajiPokok
        {
            get
            {
                return gajiPokok;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(GajiPokok)} must be >=0");
                }
                gajiPokok = value;
            }
        }
        public override decimal Pendapatan() => GajiPokok + base.Pendapatan();
        public override string ToString() =>
            $"Gaji Pokok = {base.ToString()}" +
            $"Gaji Pokok Pegawai = {GajiPokok:C}\n";
    }
    class PenggajianTest
    {
        static void Main()
        {
            var gajiPegawai = new GajiPegawai("John", "Smith", "111-11-1112", 800.00M);
            var upahJamPegawai = new UpahJamPegawai("Karen", "Price", "222-22-2222", 16.75M, 40.00M);
            var komisiPegawai = new KomisiPegawai("Sue", "Jones", "333-33-3333", 10000.00M, .06M);
            var komisiPlusGajiPokokPegawai = new KomisiPlusGajiPokokPegawai("Bob", "Lewis","444-44-4444", 5000.00M, .04M, 300.00M);

            Console.WriteLine("Karyawan Diproses Satu Persatu\n");
            Console.WriteLine($"{gajiPegawai}Diperoleh Total Gaji Pegawai = " + $"{gajiPegawai.Pendapatan():C}\n");
            Console.WriteLine($"{upahJamPegawai}Diperoleh Total Gaji Pegawai = " + $"{upahJamPegawai.Pendapatan():C}\n");
            Console.WriteLine($"{komisiPegawai}Diperoleh Total Gaji Pegawai = " + $"{komisiPegawai.Pendapatan():C}\n");
            Console.WriteLine($"{komisiPlusGajiPokokPegawai}Diperoleh Total Gaji Pegawai = " + $"{komisiPlusGajiPokokPegawai.Pendapatan():C}\n");

            var pegawai1 = new List<Pegawai>() { gajiPegawai, upahJamPegawai, komisiPegawai, komisiPlusGajiPokokPegawai };
            Console.WriteLine("Karyawan Diproses Secara Polymorphically\n");

            foreach (var currentPegawai in pegawai1)
            {
                Console.WriteLine(currentPegawai);

                if (currentPegawai is KomisiPlusGajiPokokPegawai)
                {
                    var pegawai = (KomisiPlusGajiPokokPegawai) currentPegawai;

                   pegawai.GajiPokok *= 1.10M;
                    Console.WriteLine($"Gaji pokok baru dengan kenaikan 10% = " + $"{pegawai.GajiPokok:C}");
                }
                Console.WriteLine($"Diperoleh Total Gaji Karyawan = {currentPegawai.Pendapatan():C}\n");
            }
            for (int j = 0; j < pegawai1.Count; j++)
            {
                Console.WriteLine($"Pegawai {j} adalah {pegawai1[j].GetType()}");
            }
            Console.ReadLine();
        }
    }
}
