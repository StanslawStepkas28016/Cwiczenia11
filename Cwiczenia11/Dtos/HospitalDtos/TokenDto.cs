namespace Cwiczenia11.Dtos.HospitalDtos;

public class TokenDto
{
    public string? JwtTokenString { get; set; }

    public Guid RefreshToken { get; set; }
}