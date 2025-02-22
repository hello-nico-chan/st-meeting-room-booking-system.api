namespace MeetingRoomBookingSystem.Core.Constants;

public class Token
{
    public const string Issuer = "shinetech/mrbs"; // 颁发者
    
    public const string Audience = "shinetech/mrbs"; // 接收者
    
    public const string Secret = "xnqtdMGVHfd1XgQSJNTt0Hk70V92IC1SdERfXt9rc499tAgpmrIg9Yzk6YfF"; // 签名秘钥
    
    public const int AccessExpiration = 480; // AccessToken 过期时间（分钟）
}
