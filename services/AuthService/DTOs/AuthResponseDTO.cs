class AuthResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDTO User { get; set; } = new();
    
}