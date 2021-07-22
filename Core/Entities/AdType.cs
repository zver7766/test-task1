﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
