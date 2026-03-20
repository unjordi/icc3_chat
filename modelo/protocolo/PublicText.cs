using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de texto.
/// </summary>
public class PublicText
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.PUBLIC_TEXT;
    
	/// <summary>
    /// text es el mensaje enviado
    /// </summary>
    public string text { get; set; }
    
    /// <summary>
    /// Constructor de mensaje Text.
    /// </summary>
    /// <param name="mensaje">El mensaje a enviar.</param>
    public PublicText(string mensaje)
    {
		this.text = mensaje;
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