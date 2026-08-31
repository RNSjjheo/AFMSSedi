using System.Text;

namespace AFMSDll
{
    public sealed class FbtRSETUP : _FBTableBase
    {
        public const string TABLE_NAME = "RSETUP";

        public const string COL_PK1 = "PK1";
        public const string COL_PK2 = "PK2";
        public const string COL_VALUE01 = "VALUE01";
        public const string COL_VALUE02 = "VALUE02";
        public const string COL_VALUE03 = "VALUE03";
        public const string COL_VALUE04 = "VALUE04";
        public const string COL_VALUE05 = "VALUE05";
        public const string COL_VALUE06 = "VALUE06";
        public const string COL_VALUE07 = "VALUE07";
        public const string COL_VALUE08 = "VALUE08";
        public const string COL_VALUE09 = "VALUE09";
        public const string COL_VALUE10 = "VALUE10";
        public const string COL_VALUE11 = "VALUE11";
        public const string COL_VALUE12 = "VALUE12";
        public const string COL_VALUE13 = "VALUE13";
        public const string COL_VALUE14 = "VALUE14";
        public const string COL_VALUE15 = "VALUE15";
        public const string COL_VALUE16 = "VALUE16";
        public const string COL_VALUE17 = "VALUE17";
        public const string COL_VALUE18 = "VALUE18";
        public const string COL_VALUE19 = "VALUE19";
        public const string COL_VALUE20 = "VALUE20";
        public const string COL_DESCRIPTION = "DES";

        public override string GetTableName()
        {
            return TABLE_NAME;
        }

        public override string GetCreateTableSql()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE TABLE {TABLE_NAME} (");
            sql.AppendLine($"{COL_PK1} INTEGER NOT NULL,");
            sql.AppendLine($"{COL_PK2} INTEGER NOT NULL,");
            sql.AppendLine($"{COL_VALUE01} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE02} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE03} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE04} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE05} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE06} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE07} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE08} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE09} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE10} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE11} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE12} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE13} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE14} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE15} VARCHAR(100),");
            sql.AppendLine($"{COL_VALUE16} VARCHAR(1000),");
            sql.AppendLine($"{COL_VALUE17} VARCHAR(1000),");
            sql.AppendLine($"{COL_VALUE18} VARCHAR(1000),");
            sql.AppendLine($"{COL_VALUE19} VARCHAR(1000),");
            sql.AppendLine($"{COL_VALUE20} VARCHAR(1000),");
            sql.AppendLine($"{COL_DESCRIPTION} VARCHAR(100),");
            sql.AppendLine($"CONSTRAINT PK_{TABLE_NAME} PRIMARY KEY ({COL_PK1}, {COL_PK2})");
            sql.Append(')');

            return sql.ToString();
        }
    }
}
