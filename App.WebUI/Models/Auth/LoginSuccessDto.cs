namespace App.WebUI.Models.Auth
{
    public class LoginSuccessDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Role { get; set; }   // yoksa null kalabilir
        public string? Token { get; set; }  // şimdilik opsiyonel
    }
}
