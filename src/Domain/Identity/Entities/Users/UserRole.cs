using CarRental.Domain.Common.Exceptions;

namespace CarRental.Domain.Identity.Entities.Users;
    public sealed partial class UserRole
    {
        public long UserId { get; private set; }
        public short RoleId { get; private set; }
        public DateTime AssignedAtUtc { get; private set; }

        private UserRole() { }

        internal UserRole(short roleId, DateTime nowUtc)
        {
            if (roleId <= 0)
                throw new DomainException(nameof(roleId));

            RoleId = roleId;
            AssignedAtUtc = nowUtc;
        }
    }
