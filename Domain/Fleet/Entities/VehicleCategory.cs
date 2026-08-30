using CarRental.Domain.Rentals.Entities;

namespace CarRental.Domain.Fleet.Entities;

public partial class VehicleCategory
{
    public int VehicleCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte PassengerCapacity { get; set; }

    public byte LuggageCapacity { get; set; }

    public decimal DailyRate { get; set; }

    public decimal? WeeklyRate { get; set; }

    public decimal SecurityDeposit { get; set; }

    public decimal LateFeePerHour { get; set; }

    public byte MinimumDriverAge { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
