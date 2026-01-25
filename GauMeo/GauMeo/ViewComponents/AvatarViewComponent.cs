using Microsoft.AspNetCore.Mvc;

namespace GauMeo.ViewComponents
{
    public class AvatarViewComponent : ViewComponent
    {
        /// <summary>
        /// Tạo màu avatar deterministic từ username (cùng username sẽ có cùng màu)
        /// </summary>
        private string GetAvatarColor(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return "#4769ff"; // Màu mặc định

            // Danh sách màu đẹp
            string[] colors = new[]
            {
                "#4769ff", // Xanh dương
                "#ff4757", // Đỏ hồng
                "#47ff57", // Xanh lá
                "#ffb847", // Cam
                "#b847ff", // Tím
                "#47ffb8", // Ngọc
                "#ff47b8", // Hồng
                "#47b8ff"  // Xanh biển
            };

            // Tính hash từ username để chọn màu deterministic
            int hash = userName.GetHashCode();
            int index = Math.Abs(hash) % colors.Length;
            return colors[index];
        }

        /// <summary>
        /// Lấy chữ cái đầu của username
        /// </summary>
        private string GetAvatarInitial(string userName, string? fullName = null)
        {
            if (!string.IsNullOrEmpty(fullName))
                return fullName.Substring(0, 1).ToUpper();
            if (!string.IsNullOrEmpty(userName))
                return userName.Substring(0, 1).ToUpper();
            return "U";
        }

        public IViewComponentResult Invoke(string? avatarUrl, string? userName, string? fullName = null, string? cssClass = null)
        {
            var model = new AvatarViewModel
            {
                AvatarUrl = avatarUrl,
                UserName = userName ?? "",
                FullName = fullName,
                Initial = GetAvatarInitial(userName ?? "", fullName),
                BackgroundColor = GetAvatarColor(userName ?? ""),
                CssClass = cssClass ?? ""
            };

            return View(model);
        }
    }

    public class AvatarViewModel
    {
        public string? AvatarUrl { get; set; }
        public string UserName { get; set; } = "";
        public string? FullName { get; set; }
        public string Initial { get; set; } = "U";
        public string BackgroundColor { get; set; } = "#4769ff";
        public string CssClass { get; set; } = "";
    }
}
