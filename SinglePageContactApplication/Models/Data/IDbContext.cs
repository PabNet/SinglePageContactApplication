
using System.Collections.Generic;

namespace SinglePageContactApplication.Models.Data
{
    public interface IDbContext
    {
        private protected const string DbPath = "server=localhost;user=root;password=Shazam10001001;database=Contacts;",
            LogFilePath = "Models/Data/DbReport.txt";
        
        
        
        private protected enum VersionValues : byte
        {
            Major = 8,
            Minor = 0,
            Build = 27
        }

        void DeleteDataBase();
    }
}