namespace Cwiczenia11.ModelsDtos.HospitalDtos;

public class TokenDto
{
    public string? JwtTokenString { get; set; }

    public Guid RefreshToken { get; set; }
}