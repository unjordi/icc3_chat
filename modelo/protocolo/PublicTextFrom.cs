using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de texto.
/// </summary>
public class PublicTextFrom
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.PUBLIC_TEXT_FROM;
    
    /// <summary>
    /// username es el nombre de usuario destinatario
    /// </summary>
    public string username { get; set; }
	
	/// <summary>
    /// text es el mensaje enviado
    /// </summary>
    public string text { get; set; }
    
    /// <summary>
    /// Constructor de mensaje Text.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    /// <param name="mensaje">El mensaje a enviar.</param>
    public PublicTextFrom(string username, string mensaje)
    {
		this.username = username;
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