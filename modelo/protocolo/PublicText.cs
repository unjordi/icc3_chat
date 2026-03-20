using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de texto público.
/// </summary>
/// <remarks>
/// Constructor de mensaje Text.
/// </remarks>
/// <param name="mensaje">El mensaje a enviar.</param>
public class PublicText(string mensaje)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.PUBLIC_TEXT;

	/// <summary>
	/// text es el mensaje enviado
	/// </summary>
	public string text { get; set; } = mensaje;

	public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}