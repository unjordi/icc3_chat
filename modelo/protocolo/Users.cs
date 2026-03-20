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
    public typeOptions type {get;} = typeOptions.USERS;
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}