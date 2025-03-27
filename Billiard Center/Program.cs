using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Rental> rentals = new List<Rental>();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Aplikasi Penyewaan Billiard ===");
            Console.WriteLine("1. Sewa Meja");
            Console.WriteLine("2. Lihat Penyewaan Aktif");
            Console.WriteLine("3. Akhiri Penyewaan");
            Console.WriteLine("4. Keluar");
            Console.Write("Pilih menu: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RentTable();
                    break;
                case "2":
                    ShowActiveRentals();
                    break;
                case "3":
                    EndRental();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid!");
                    break;
            }
            Console.WriteLine("\nTekan ENTER untuk kembali ke menu...");
            Console.ReadLine();
        }
    }

    static void RentTable()
    {
        Console.Write("Nama Penyewa: ");
        string customerName = Console.ReadLine();

        Console.Write("Nomor Meja: ");
        int tableNumber;
        while (!int.TryParse(Console.ReadLine(), out tableNumber))
        {
            Console.Write("Harap masukkan angka untuk nomor meja: ");
        }

        rentals.Add(new Rental
        {
            Id = rentals.Count + 1,
            CustomerName = customerName,
            TableNumber = tableNumber,
            StartTime = DateTime.Now
        });

        Console.WriteLine($"Meja {tableNumber} berhasil disewa oleh {customerName}.");
    }

    static void ShowActiveRentals()
    {
        Console.WriteLine("\n=== Penyewaan Aktif ===");
        if (rentals.Count == 0)
        {
            Console.WriteLine("Tidak ada penyewaan aktif.");
            return;
        }

        foreach (var rental in rentals.Where(r => r.EndTime == null))
        {
            Console.WriteLine($"ID: {rental.Id} | Meja: {rental.TableNumber} | Nama: {rental.CustomerName} | Mulai: {rental.StartTime}");
        }
    }

    static void EndRental()
    {
        Console.Write("Masukkan ID penyewaan yang ingin diakhiri: ");
        int rentalId;
        while (!int.TryParse(Console.ReadLine(), out rentalId))
        {
            Console.Write("Harap masukkan angka: ");
        }

        var rental = rentals.FirstOrDefault(r => r.Id == rentalId && r.EndTime == null);
        if (rental == null)
        {
            Console.WriteLine("ID penyewaan tidak ditemukan atau sudah diakhiri.");
            return;
        }

        rental.EndTime = DateTime.Now;
        rental.TotalCost = (decimal)(rental.EndTime.Value - rental.StartTime).TotalHours * 50000; // Rp 50.000/jam
        Console.WriteLine($"Penyewaan meja {rental.TableNumber} oleh {rental.CustomerName} telah berakhir.");
        Console.WriteLine($"Total biaya: Rp {rental.TotalCost:N0}");
    }
}

class Rental
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public int TableNumber { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal TotalCost { get; set; }
}
