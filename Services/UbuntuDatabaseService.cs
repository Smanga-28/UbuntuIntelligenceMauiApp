using SQLite;

namespace Ubuntu_Intelli_Compass.Services
{
    public class UbuntuDatabaseService
    {
        private const string DbFileName = "UbuntuIntelliCompass.db";

        private readonly string _dbFilePath;
        private readonly SemaphoreSlim _lock = new(1, 1);
        private SQLiteAsyncConnection? _connection;

        public UbuntuDatabaseService()
        {
            _dbFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, DbFileName);
        }

        public async Task<SQLiteAsyncConnection> GetDbConnectionAsync()
        {
            if (_connection != null)
                return _connection;

            // Preload and Register can both ask for the connection at nearly the
            // same time, so only let one caller set it up.
            await _lock.WaitAsync();
            try
            {
                if (_connection != null)
                    return _connection;

                if (!File.Exists(_dbFilePath))
                    await CopyBundledDatabaseAsync();

                var connection = OpenConnection();

                // An older/stale copy of the DB (from an earlier run of the app)
                // would never be replaced, because the file already exists.
                // If it has no USERS table, throw it away and copy the bundled one again.
                if (!await HasUsersTableAsync(connection))
                {
                    await connection.CloseAsync();
                    File.Delete(_dbFilePath);
                    await CopyBundledDatabaseAsync();
                    connection = OpenConnection();

                    if (!await HasUsersTableAsync(connection))
                        throw new InvalidOperationException(
                            "The bundled database has no USERS table. Check Resources/Raw/UbuntuIntelliCompass.db.");
                }

                _connection = connection;
                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        private SQLiteAsyncConnection OpenConnection()
            => new SQLiteAsyncConnection(_dbFilePath, storeDateTimeAsTicks: false);

        private static async Task<bool> HasUsersTableAsync(SQLiteAsyncConnection connection)
        {
            int count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = 'USERS'");
            return count > 0;
        }

        private async Task CopyBundledDatabaseAsync()
        {
            // 1) The database is embedded in the app (see the csproj).
            Stream? inputStream = typeof(UbuntuDatabaseService).Assembly
                .GetManifestResourceStream(DbFileName);

            // 2) Fallback: the MAUI "Raw" asset folder.
            if (inputStream == null)
            {
                try
                {
                    inputStream = await FileSystem.OpenAppPackageFileAsync(DbFileName);
                }
                catch (Exception ex)
                {
                    throw new FileNotFoundException(
                        $"{DbFileName} was not found inside the app. " +
                        "Make sure Resources/Raw/UbuntuIntelliCompass.db exists and rebuild.", ex);
                }
            }

            using (inputStream)
            using (FileStream outputStream = File.Create(_dbFilePath))
            {
                await inputStream.CopyToAsync(outputStream);
            }
        }
    }
}
