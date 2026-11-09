namespace CarRental.Application.Common.Constant;

public static class RefreshTokenRevocationReasons
{
    public const string Logout = "User logged out";
    public const string Rotated = "Refresh token rotated";
    public const string ReuseDetected = "Refresh token reuse detected";
    public const string AdminRevoked = "Revoked by administrator";
}