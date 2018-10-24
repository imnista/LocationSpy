namespace LocationSpy.Models
{
    using System.ComponentModel;

    public enum PlatformType
    {
        [Description("未知")]
        Unknown,

        [Description("Windows")]
        Windows,

        [Description("Apple")]
        Apple,

        [Description("Android")]
        Android
    }
}