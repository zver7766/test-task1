using System.Runtime.Serialization;

namespace Core.Entities
{
    public enum AdType
    {
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
