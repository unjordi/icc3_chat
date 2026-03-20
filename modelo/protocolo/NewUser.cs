using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un aviso de ingreso de usuario al chat.
/// </summary>
/// <remarks>
/// Constructor de mensaje NewUser.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
public class NewUser(string username)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.NEW_USER;

	/// <summary>
	/// username es el nombre de usuario.
	/// </summary>
	public string username { get; set; } = username;

	public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}