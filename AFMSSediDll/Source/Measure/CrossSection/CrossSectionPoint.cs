using System;
using System.Collections.Generic;
using System.Text;

namespace AFMSSediDll
{
    public class CrossSectionPoint
    {
        public double LeftBankDistance;
        public double Elevation;

        public CrossSectionPoint()
        {
        }

        public CrossSectionPoint(double leftBankDistance, double elevation)
        {
            LeftBankDistance = leftBankDistance;
            Elevation = elevation;
        }
    }
}
