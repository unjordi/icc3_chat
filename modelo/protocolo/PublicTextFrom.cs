using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de texto.
/// </summary>
public class PublicTextFrom
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.IDENTIFY;
    
    /// <summary>
    /// username es el nombre de usuario.
    /// </summary>
    public string username {get;set;}
    
    /// <summary>
    /// Constructor de mensaje PublicTextFrom.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    public PublicTextFrom(string username)
    {
        this.username = username;
    }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}