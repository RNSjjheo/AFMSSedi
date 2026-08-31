using System;
using System.Collections.Generic;
using System.Text;

namespace AFMSSediDll
{
    public enum PacketParseResult
    {
        NeedMoreData,
        PacketReceived,
        InvalidData
    }
}
