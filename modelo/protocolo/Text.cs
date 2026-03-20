using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de texto.
/// </summary>
/// <remarks>
/// Constructor de mensaje Text.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
/// <param name="mensaje">El mensaje a enviar.</param>
public class Text(string username, string mensaje)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.TEXT;

	/// <summary>
	/// username es el nombre de usuario destinatario
	/// </summary>
	public string username { get; set; } = username;

	/// <summary>
	/// text es el mensaje enviado
	/// </summary>
	public string text { get; set; } = mensaje;

	public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}