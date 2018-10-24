namespace LocationSpy.Models
{
    #region using directives

    using SquirrelFramework.Domain.Model;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    #endregion using directives

    [Collection("Locator")]
    public class LocatorItem : DomainModel
    {
        [DisplayName("新建项名称")]
        [Required(ErrorMessage = "请输入新建的项目名称")]
        public string Name { get; set; }

        [DisplayName("用户重定向网址")]
        [Required(ErrorMessage = "请输入用户重定向网址")]
        public string RedirectUrl { get; set; }

        [DisplayName("用户重定向标题")]
        public string RedirectTitle { get; set; }

        public string Identifier { get; set; }

        public PlatformType PlatformType { get; set; }
        public Geolocation Location { get; set; }
        public string AuxiliaryLocationInformation { get; set; }
        public string IPAddress { get; set; }
        public CurrentStatus Status { get; set; }

        public DateTime LastModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Creator { get; set; }
    }
}