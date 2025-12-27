namespace HealthcareApp.Domain.Constants;

public class UserRolesConstants
{
    public const string Doctor = "Doctor";
    public const string Patient = "Patient";

    public static readonly string[] AllowedRoles = new[] { "Doctor", "Patient" };

    public static bool IsRoleAllowed(string role) => AllowedRoles.Contains(role);
}
