using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpillTracker.Data
{
    /// <summary>
    /// Helper class to hold all the information we need to construct the users for this app.  Basically
    /// a union of FujiUser and IdentityUser.  Not great to have to duplicate this data but it is only for one-time seeding
    /// of the databases.  Does NOT hold passwords since we need to store those in a secret location.
    /// </summary>
    public class UserInfoData
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public string Role { get; set; }

        public int? CompanyID { get; set; }

        public int[] StuserFacilities { get; set; }
    }

    public class SeedData
    {
        /// <summary>
        /// Data to be used to seed the FujiUsers and ASPNetUsers tables
        /// </summary>
        public static readonly UserInfoData[] UserSeedData = new UserInfoData[]
        {
            // Test Company 1 Users
            new UserInfoData { UserName = "TaliaK", Email = "knott@example.com", FirstName = "Talia", LastName = "Knott", Role = "FacilityManager", CompanyID = 1, StuserFacilities = new int[] {1,3} },
            new UserInfoData { UserName = "ZaydenC", Email = "clark@example.com", FirstName = "Zayden", LastName = "Clark", Role = "Employee", CompanyID = 1, StuserFacilities = new int[] {1}},

            // Test Company 2 Users
            new UserInfoData { UserName = "DavilaH", Email = "hareem@example.com", FirstName = "Hareem", LastName = "Davila", Role = "FacilityManager", CompanyID = 2, StuserFacilities = new int[] {2} },
            new UserInfoData { UserName = "KrzysztofP", Email = "krzysztof@example.com", FirstName = "Krzysztof", LastName = "Ponce", Role = "Employee", CompanyID = 2, StuserFacilities = new int[] {2} },

            // Users with No Company and Facilities
            new UserInfoData { UserName = "SmithT", Email = "smitht@example.com", FirstName = "Ty", LastName = "Smith", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "JohnH", Email = "johnh@example.com", FirstName = "John", LastName = "Hammer", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "JacobJ", Email = "jacobj@example.com", FirstName = "Jacob", LastName = "Jingleheimer", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "MatS", Email = "mats@example.com", FirstName = "Mat", LastName = "Schmit", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "AlexD", Email = "alexd@example.com", FirstName = "Alex", LastName = "DeMeo", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "LaurenM", Email = "laurenm@example.com", FirstName = "Lauren", LastName = "Michael", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "JoshM", Email = "joshm@example.com", FirstName = "Nathan", LastName = "McNeil", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "ShelbyS", Email = "shelbys@example.com", FirstName = "Shelby", LastName = "Straws", Role = "FacilityManager", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "RandyK", Email = "randyk@example.com", FirstName = "Randy", LastName = "Kraft", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "TrentS", Email = "trents@example.com", FirstName = "Trent", LastName = "Spring", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "MckaylaM", Email = "mckaylam@example.com", FirstName = "Mckayla", LastName = "Martin", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "LizL", Email = "lizl@example.com", FirstName = "Liz", LastName = "Liddel", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "JackL", Email = "jackl@example.com", FirstName = "Jack", LastName = "Lee", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "TerryB", Email = "terryb@example.com", FirstName = "Terry", LastName = "Bird", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "TomM", Email = "tomm@example.com", FirstName = "Tom", LastName = "McGraw", Role = "Employee", CompanyID = null, StuserFacilities = null },
            new UserInfoData { UserName = "JackieO", Email = "jackieo@example.com", FirstName = "Jackie", LastName = "Oneida", Role = "Employee", CompanyID = null, StuserFacilities = null }
        };
    }
}
