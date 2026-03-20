using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de desconexión
/// </summary>
/// <remarks>
/// Constructor de mensaje de desconexión
/// </remarks>
/// <param name="username">El nombre de usuario que se desconectó.</param>
public class Disconected(string username)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.DISCONNECTED;

	/// <summary>
	/// text es el mensaje enviado
	/// </summary>
	public string username { get; set; } = username;

	public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}