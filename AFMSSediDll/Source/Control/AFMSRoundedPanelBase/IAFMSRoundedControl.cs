using System.Drawing;

namespace AFMSSediDll
{
    public interface IAFMSRoundedControl
    {
        int BorderRadius { get; set; }
        Color BorderColor { get; set; }
        float BorderThickness { get; set; }
    }
}
