namespace CarRental.Domain.Identity.Entities
{
    public partial class UserRole
    {
        public long UserId { get; set; }
        public short RoleId { get; set; }
        public DateTime AssignedAtUtc { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
