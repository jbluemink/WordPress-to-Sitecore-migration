using Newtonsoft.Json;

public class Authentication
{
    [JsonProperty("domain")]
    public string Domain { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }
}