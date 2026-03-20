using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de INVITATION.
/// </summary>
/// <remarks>
/// Constructor de mensaje Invitation.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
public class Invitation(string username,string roomname)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.INVITATION;

	/// <summary>
	/// username es el nombre de usuario.
	/// </summary>
	public string username { get; set; } = username;
	/// <summary>
	/// username es el nombre de usuario.
	/// </summary>
	public string roomname { get; set; } = roomname;

	public override string ToString()
	{
		return JsonSerializer.Serialize(this);
	}
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}