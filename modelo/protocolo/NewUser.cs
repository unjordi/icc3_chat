using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un avisod de ingreso de usuario al chat.
/// </summary>
public class NewUser
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.NEW_USER;
    
    /// <summary>
    /// username es el nombre de usuario.
    /// </summary>
    public string username {get;set;}
    
    /// <summary>
    /// Constructor de mensaje NewUser.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    public NewUser(string username)
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