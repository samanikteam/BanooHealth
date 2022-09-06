using System.Collections.Generic;

namespace Data.Models.Constants
{
public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.Users",
            $"Permissions.{module}.Blogs",
            $"Permissions.{module}.Product",
            $"Permissions.{module}.Resaneh",
            $"Permissions.{module}.DarooKhaneh",
            $"Permissions.{module}.Setting",
            $"Permissions.{module}.PanelDarookhoneh",
        };
    }

    public static class Samanik
    {
        public const string Users = "Permissions.Samanik.Users";
        public const string Blogs = "Permissions.Samanik.Blogs";
        public const string Product = "Permissions.Samanik.Product";
        public const string Resaneh = "Permissions.Samanik.Resaneh";
        public const string DarooKhaneh = "Permissions.Samanik.DarooKhaneh";
        public const string Setting = "Permissions.Samanik.Setting";
        public const string PanelDarookhoneh = "Permissions.Samanik.PanelDarookhoneh";
    }
}
}