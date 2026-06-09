using System;
using System.Collections.Generic;

//1
abstract class PesananTransportasi
{
    
    private string namaPenumpang;
    private string idPesanan;
    private string lokasiTujuan;

    
    public PesananTransportasi(string namaPenumpang, string idPesanan, string lokasiTujuan)
    {
        this.namaPenumpang = namaPenumpang;
        this.idPesanan = idPesanan;
        this.lokasiTujuan = lokasiTujuan;
    }

    
    public string NamaPenumpang
    {
        get { return namaPenumpang; }
        set { namaPenumpang = value; }
    }

    public string IdPesanan
    {
        get { return idPesanan; }
        set { idPesanan = value; }
    }

    public string LokasiTujuan
    {
        get { return lokasiTujuan; }
        set { lokasiTujuan = value; }
    }


    public void TampilInfo()
    {
        Console.WriteLine($"Nama: {namaPenumpang} | ID: {idPesanan} | Tujuan: {lokasiTujuan}");
    }

    
    public abstract double HitungTarif(double jarakKm);
}

//2 , 3 dan 4
class LayananMotor : PesananTransportasi
{
    private double tarifPerKm;

    public LayananMotor(string namaPenumpang, string idPesanan, string lokasiTujuan, double tarifPerKm)
        : base(namaPenumpang, idPesanan, lokasiTujuan)
    {
        this.tarifPerKm = tarifPerKm;
    }

    public double TarifPerKm
    {
        get { return tarifPerKm; }
        set { tarifPerKm = value; }
    }

    
    public override double HitungTarif(double jarakKm)
    {
        return jarakKm * tarifPerKm;
    }

    public void TampilDetail(double jarakKm)
    {
        TampilInfo();
        Console.WriteLine($"Total: Rp {HitungTarif(jarakKm):N0}");
    }
}


class LayananMobil : PesananTransportasi
{
    private double tarifPerKm;
    private double biayaTol;

    public LayananMobil(string namaPenumpang, string idPesanan, string lokasiTujuan,
                        double tarifPerKm, double biayaTol)
        : base(namaPenumpang, idPesanan, lokasiTujuan)
    {
        this.tarifPerKm = tarifPerKm;
        this.biayaTol = biayaTol;
    }

    public double TarifPerKm
    {
        get { return tarifPerKm; }
        set { tarifPerKm = value; }
    }

    public double BiayaTol
    {
        get { return biayaTol; }
        set { biayaTol = value; }
    }

    
    public override double HitungTarif(double jarakKm)
    {
        return (jarakKm * tarifPerKm) + biayaTol;
    }

    public void TampilDetail(double jarakKm)
    {
        TampilInfo();
        Console.WriteLine($"Total: Rp {HitungTarif(jarakKm):N0}");
    }
}

//5
class RiwayatPerjalanan
{
    private class EntriPerjalanan
    {
        public string JenisLayanan { get; set; }
        public double JarakKm { get; set; }
        public string TanggalPesan { get; set; }
        public string NamaPenumpang { get; set; }
        public string IdPesanan { get; set; }
        public string LokasiTujuan { get; set; }
        public double TotalTarif { get; set; }
    }

    private List<EntriPerjalanan> daftarPerjalanan = new List<EntriPerjalanan>();
    
    public void TambahPerjalanan(LayananMotor pesanan, double jarakKm, string tanggalPesan)
    {
        daftarPerjalanan.Add(new EntriPerjalanan
        {
            JenisLayanan = "Motor",
            JarakKm = jarakKm,
            TanggalPesan = tanggalPesan,
            NamaPenumpang = pesanan.NamaPenumpang,
            IdPesanan = pesanan.IdPesanan,
            LokasiTujuan = pesanan.LokasiTujuan,
            TotalTarif = pesanan.HitungTarif(jarakKm)
        });
    }

    public void TambahPerjalanan(LayananMobil pesanan, double jarakKm, string tanggalPesan)
    {
        daftarPerjalanan.Add(new EntriPerjalanan
        {
            JenisLayanan = "Mobil",
            JarakKm = jarakKm,
            TanggalPesan = tanggalPesan,
            NamaPenumpang = pesanan.NamaPenumpang,
            IdPesanan = pesanan.IdPesanan,
            LokasiTujuan = pesanan.LokasiTujuan,
            TotalTarif = pesanan.HitungTarif(jarakKm)
        });
    }

    
    public void CetakRiwayat()
    {
        int no = 1;
        foreach (var entry in daftarPerjalanan)
        {
            Console.WriteLine($"{no++}. {entry.JenisLayanan} | {entry.JarakKm} km | {entry.TanggalPesan}");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        LayananMobil mobil = new LayananMobil(
            namaPenumpang: "Budi",
            idPesanan: "TRX01",
            lokasiTujuan: "Stasiun",
            tarifPerKm: 6000,
            biayaTol: 5000
        );

        
        mobil.TampilDetail(jarakKm: 10);

        
        RiwayatPerjalanan riwayat = new RiwayatPerjalanan();
        riwayat.TambahPerjalanan(mobil, 10, "10-10-2025");
        riwayat.CetakRiwayat();
    }
}