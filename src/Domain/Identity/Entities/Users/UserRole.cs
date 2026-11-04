using CarRental.Domain.Common.Exceptions;
using CarRental.Domain.Identity.Entities.Roles;

namespace CarRental.Domain.Identity.Entities.Users;
    public sealed partial class UserRole
    {
        public long UserId { get; private set; }
        public short RoleId { get; private set; }
        public DateTime AssignedAtUtc { get; private set; }

        public Role Role { get; private set; } = null!;

        private UserRole() { }

        internal UserRole(short roleId, DateTime nowUtc)
        {
            if (roleId <= 0)
                throw new DomainException(nameof(roleId));

            RoleId = roleId;
            AssignedAtUtc = nowUtc;
        }
    }
