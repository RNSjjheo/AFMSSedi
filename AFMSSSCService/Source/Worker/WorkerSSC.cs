using AFMSSediDll;
using System.Data;

namespace AFMSSSCService
{
    public class WorkerSSC(ILogger<WorkerSSC> logger) : BackgroundService
    {
        private RSandProfileSnapshot? profile;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            profile = LoadLatestProfile();

            logger.LogInformation(
                "SSC 프로파일을 메모리에 로드했습니다. " +
                "ProfileId={ProfileId}, " +
                "A={ADeviceType}({ACellFrom}~{ACellTo}), " +
                "B={BDeviceType}({BCellFrom}~{BCellTo})",
                profile.ProfileId,
                profile.A.DeviceType,
                profile.A.CellFrom,
                profile.A.CellTo,
                profile.B.DeviceType,
                profile.B.CellFrom,
                profile.B.CellTo);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation(
                        "Worker running at: {time}, SSC ProfileId: {profileId}",
                        DateTimeOffset.Now,
                        profile.ProfileId);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private static RSandProfileSnapshot LoadLatestProfile()
        {
            string sql = "SELECT FIRST 1";
            sql += $" {FbtRSANDPROFILE.COL_PROFILE_ID},";
            sql += $" {FbtRSANDPROFILE.COL_PROFILE_DATE},";
            sql += $" {FbtRSANDPROFILE.COL_PROFILE_TIME},";
            sql += $" {FbtRSANDPROFILE.COL_PROFILE_NAME},";
            sql += $" {FbtRSANDPROFILE.COL_A_SETUP_FLAG},";
            sql += $" {FbtRSANDPROFILE.COL_A_DEVICE_TYPE},";
            sql += $" {FbtRSANDPROFILE.COL_A_VALID_CELL_TYPE},";
            sql += $" {FbtRSANDPROFILE.COL_A_CELL_FROM},";
            sql += $" {FbtRSANDPROFILE.COL_A_CELL_TO},";
            sql += $" {FbtRSANDPROFILE.COL_A_DB_FROM},";
            sql += $" {FbtRSANDPROFILE.COL_A_DB_TO},";
            sql += $" {FbtRSANDPROFILE.COL_A_REGRESSION},";
            sql += $" {FbtRSANDPROFILE.COL_A_K_VALUE},";
            sql += $" {FbtRSANDPROFILE.COL_A_BEAM_ANGLE},";
            sql += $" {FbtRSANDPROFILE.COL_A_SSC_A},";
            sql += $" {FbtRSANDPROFILE.COL_A_SSC_B},";
            sql += $" {FbtRSANDPROFILE.COL_B_SETUP_FLAG},";
            sql += $" {FbtRSANDPROFILE.COL_B_DEVICE_TYPE},";
            sql += $" {FbtRSANDPROFILE.COL_B_VALID_CELL_TYPE},";
            sql += $" {FbtRSANDPROFILE.COL_B_CELL_FROM},";
            sql += $" {FbtRSANDPROFILE.COL_B_CELL_TO},";
            sql += $" {FbtRSANDPROFILE.COL_B_DB_FROM},";
            sql += $" {FbtRSANDPROFILE.COL_B_DB_TO},";
            sql += $" {FbtRSANDPROFILE.COL_B_REGRESSION},";
            sql += $" {FbtRSANDPROFILE.COL_B_K_VALUE},";
            sql += $" {FbtRSANDPROFILE.COL_B_BEAM_ANGLE},";
            sql += $" {FbtRSANDPROFILE.COL_B_SSC_A},";
            sql += $" {FbtRSANDPROFILE.COL_B_SSC_B}";
            sql += $" FROM {FbtRSANDPROFILE.TABLE_NAME}";
            sql += $" ORDER BY {FbtRSANDPROFILE.COL_PROFILE_ID} DESC";

            using FBDatabase db = FBProvider.Instance.CreateDatabase();
            string error = db.RunQuery(sql);

            if (!string.IsNullOrEmpty(error))
            {
                throw new InvalidOperationException(
                    $"{FbtRSANDPROFILE.TABLE_NAME} 조회에 실패했습니다.\n{error}");
            }

            if (db.Results.Rows.Count == 0)
            {
                throw new InvalidOperationException(
                    $"{FbtRSANDPROFILE.TABLE_NAME}에 SSC 연산 프로파일이 없습니다.");
            }

            return CreateSnapshot(db.Results.Rows[0]);
        }

        private static RSandProfileSnapshot CreateSnapshot(DataRow row)
        {
            RSandDeviceProfile profileA = new RSandDeviceProfile(
                GetString(row, FbtRSANDPROFILE.COL_A_SETUP_FLAG),
                GetString(row, FbtRSANDPROFILE.COL_A_DEVICE_TYPE),
                GetString(row, FbtRSANDPROFILE.COL_A_VALID_CELL_TYPE),
                GetInt32(row, FbtRSANDPROFILE.COL_A_CELL_FROM),
                GetInt32(row, FbtRSANDPROFILE.COL_A_CELL_TO),
                GetDouble(row, FbtRSANDPROFILE.COL_A_DB_FROM),
                GetDouble(row, FbtRSANDPROFILE.COL_A_DB_TO),
                GetDouble(row, FbtRSANDPROFILE.COL_A_REGRESSION),
                GetDouble(row, FbtRSANDPROFILE.COL_A_K_VALUE),
                GetDouble(row, FbtRSANDPROFILE.COL_A_BEAM_ANGLE),
                GetDouble(row, FbtRSANDPROFILE.COL_A_SSC_A),
                GetDouble(row, FbtRSANDPROFILE.COL_A_SSC_B));

            RSandDeviceProfile profileB = new RSandDeviceProfile(
                GetString(row, FbtRSANDPROFILE.COL_B_SETUP_FLAG),
                GetString(row, FbtRSANDPROFILE.COL_B_DEVICE_TYPE),
                GetString(row, FbtRSANDPROFILE.COL_B_VALID_CELL_TYPE),
                GetInt32(row, FbtRSANDPROFILE.COL_B_CELL_FROM),
                GetInt32(row, FbtRSANDPROFILE.COL_B_CELL_TO),
                GetDouble(row, FbtRSANDPROFILE.COL_B_DB_FROM),
                GetDouble(row, FbtRSANDPROFILE.COL_B_DB_TO),
                GetDouble(row, FbtRSANDPROFILE.COL_B_REGRESSION),
                GetDouble(row, FbtRSANDPROFILE.COL_B_K_VALUE),
                GetDouble(row, FbtRSANDPROFILE.COL_B_BEAM_ANGLE),
                GetDouble(row, FbtRSANDPROFILE.COL_B_SSC_A),
                GetDouble(row, FbtRSANDPROFILE.COL_B_SSC_B));

            return new RSandProfileSnapshot(
                GetInt32(row, FbtRSANDPROFILE.COL_PROFILE_ID),
                GetString(row, FbtRSANDPROFILE.COL_PROFILE_DATE),
                GetString(row, FbtRSANDPROFILE.COL_PROFILE_TIME),
                GetString(row, FbtRSANDPROFILE.COL_PROFILE_NAME),
                profileA,
                profileB);
        }

        private static string GetString(DataRow row, string columnName)
        {
            return Convert.ToString(row[columnName])?.Trim() ?? string.Empty;
        }

        private static int GetInt32(DataRow row, string columnName)
        {
            return row[columnName] == DBNull.Value ? 0 : Convert.ToInt32(row[columnName]);
        }

        private static double GetDouble(DataRow row, string columnName)
        {
            return row[columnName] == DBNull.Value ? 0.0 : Convert.ToDouble(row[columnName]);
        }
    }
}
