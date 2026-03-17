using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un avisod de ingreso de usuario al chat.
/// </summary>
public class Users
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
    /// Constructor de mensaje Users.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    public Users(string username)
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