namespace Cwiczenia11.ModelsDtos.AuthDtos;

public class TokensReponseDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public int StatusCode { get; set; }
}