namespace AFMSSSCService
{
    /// <summary>
    /// RADX SED 자료 한 행을 표현한다.
    /// 헤더에는 ADVM 및 셀 열이 한 세트만 표시되지만 실제 데이터에서는 반복된다.
    /// </summary>
    internal sealed class RadxSedimentRecord
    {
        public string StationCode { get; set; } = string.Empty; // St_code
        public string MeasurementTime { get; set; } = string.Empty; // YYYYMMDDhhmm
        public string OverallDecision { get; set; } = string.Empty; // Deci_All
        public string VthDecision { get; set; } = string.Empty; // Deci_VTH
        public int Ac { get; set; } // AC
        public double DcCharge { get; set; } // DC_Charge
        public double DcBattery { get; set; } // DC_Battery
        public double SystemTemperature { get; set; } // Temp_Sys
        public double SystemHumidity { get; set; } // Hr_Sys
        public string WaterLevelDecision { get; set; } = string.Empty; // Deci_WL
        public double WaterDepth { get; set; } // WaterDepth
        public double WaterLevel { get; set; } // WaterLevel
        public double WaterLevelOffset { get; set; } // WL_Offset
        public double Salinity { get; set; } // Salinity

        /// <summary>
        /// RADX가 출력한 ADVM 블록 목록. 현재 RADX는 하층 1번과 상층 2번을 출력할 수 있다.
        /// </summary>
        public List<RadxAdvmRecord> Advms { get; } = [];
    }

    /// <summary>
    /// No_ADVM부터 셀 데이터 직전까지의 반복 장비 블록을 표현한다.
    /// </summary>
    internal sealed class RadxAdvmRecord
    {
        public int Number { get; set; } // No_ADVM
        public int Type { get; set; } // ADVMType
        public double Ssc { get; set; } // SSC
        public double Sediment { get; set; } // Sedment (RADX 원본 철자)
        public double TotalSediment { get; set; } // TotalSed
        public int StartCell { get; set; } // StartCell
        public int EndCell { get; set; } // EndSell (RADX 원본 철자)
        public string Decision { get; set; } = string.Empty; // Dec_ADVM
        public double WaterTemperature { get; set; } // Temp_Water
        public double Depth { get; set; } // Depth_ADVM
        public double Pitch { get; set; } // Pitch
        public double Roll { get; set; } // Roll
        public int CellCount { get; set; } // WN
        public int CellSize { get; set; } // WS (cm)
        public int PingCount { get; set; } // WP
        public int Frequency { get; set; } // WF
        public int FirstCellDistance { get; set; } // DIS1 (cm)
        public int LastCellDistance { get; set; } // DIS2 (cm)

        /// <summary>
        /// No_Cell, V_EW, V_NS, E1, E2 열이 WN만큼 반복된 값이다.
        /// </summary>
        public List<RadxCellRecord> Cells { get; } = [];

        public bool HasExpectedCellCount =>
            CellCount >= 0 && Cells.Count == CellCount;
    }

    /// <summary>
    /// RADX SED 행의 반복 셀 블록을 표현한다.
    /// </summary>
    internal sealed class RadxCellRecord
    {
        public int Number { get; set; } // No_Cell
        public int VelocityEastWest { get; set; } // V_EW (mm/s)
        public int VelocityNorthSouth { get; set; } // V_NS (mm/s)
        public int Echo1 { get; set; } // E1
        public int Echo2 { get; set; } // E2
    }
}
