using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de indentificacion.
/// </summary>
public class Invitation
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
    /// Constructor de mensaje Invitation.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    public Invitation(string username)
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