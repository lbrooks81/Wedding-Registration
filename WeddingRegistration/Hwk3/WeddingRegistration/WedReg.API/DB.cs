using WedReg.Shared.Models;

namespace WedReg.API
{
    public static class DB
    {
        public static List<Registration> Registrations { get; set; } = [];
        // Include ID for each registration
    }
}
