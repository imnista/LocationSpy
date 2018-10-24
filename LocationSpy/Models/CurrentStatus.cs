namespace LocationSpy.Models
{
    using System.ComponentModel;

    public enum CurrentStatus
    {
        [Description("待访问")]
        NotReady = 100,

        [Description("GPS 发现")]
        FoundByGPS = 201,

        [Description("IP 发现")]
        FoundByIPAddress = 202,

        [Description("无法找到")]
        NotFound = 404,

        [Description("未知")]
        Unknown = 400
    }
}