using System.Text.Json;
using System.Text.Json.Serialization;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de RESPONSE.
/// </summary>
/// <remarks>
/// Constructor de mensaje RESPONSE.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
public class Response(operationOptions operation, resultOptions result, string extra)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.RESPONSE;

	/// <summary>
	/// operation es el tipo de mensaje al que se está respondiendo.
	/// </summary>
	public operationOptions operation { get; } = operation;

	/// <summary>
	/// result representa el éxito o fracaso.
	/// </summary>
	public resultOptions result { get; } = result;

	/// <summary>
	/// extra es el contenido adicional.
	/// </summary>
	public string extra { get; set; } = extra;

	public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}