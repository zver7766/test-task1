using System.Runtime.Serialization;

namespace Core.Entities
{
    public enum AdTypeToCreate
    {
        [EnumMember(Value = "Auto")]
        Auto,
        [EnumMember(Value = "TextAd")]
        TextAd,
        [EnumMember(Value = "HtmlAd")]
        HtmlAd,
        [EnumMember(Value = "BannerAd")]
        BannerAd,
        [EnumMember(Value = "VideoAd")]
        VideoAd
    }
}
