using System;
using System.Collections.Generic;
using System.Text;

namespace AFMSSSCService
{
    internal class SSCServiceOptions
    {
        public const string SectionName = "SSCSection";

        public DateTime CalculationStartTime { get; set; } =
            new(2026, 8, 25, 0, 0, 0, DateTimeKind.Local);

    }
}
