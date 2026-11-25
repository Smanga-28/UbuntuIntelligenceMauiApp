using System;
using System.Collections.Generic;
using System.Text;
using SQLite;



namespace Ubuntu_Intelli_Compass.Services
{
    public class UbuntuDatabaseService
    {
        private string dbFileName = "UbuntuIntelliCompass.db";
        private string dbFilePath = string.Empty;
        private SQLiteAsyncConnection dbConnection;

        public UbuntuDatabaseService() 
        { 
            dbFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, dbFileName);

        }
        public async Task<SQLiteAsyncConnection> GetDbConnectionAsync()
        {
            if(dbConnection != null)
            {
                return dbConnection;
            }
            if(!File.Exists(dbFilePath))
            
               await ExtractDatabaseFromMauiAsync();

            dbConnection = new SQLiteAsyncConnection(dbFilePath, storeDateTimeAsTicks: false);
            return dbConnection;
        }
        private async Task ExtractDatabaseFromMauiAsync()
        {
            using Stream inputStream = await FileSystem.OpenAppPackageFileAsync(dbFileName);
            
            using FileStream outputStream = File.Create(dbFilePath);
            await inputStream.CopyToAsync(outputStream);

        }

    }
}
