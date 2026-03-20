using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de indentificacion.
/// </summary>
/// <remarks>
/// Constructor de mensaje IDENTIFY.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
public class Identify(string username)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public static typeOptions type => typeOptions.IDENTIFY;

	/// <summary>
	/// username es el nombre de usuario.
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