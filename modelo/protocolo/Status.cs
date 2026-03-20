using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de cambio de estado
/// </summary>
/// <remarks>
/// Constructor de mensaje Status
/// </remarks>
/// <param name="nuevoEstado">El nuevo estado deseado por el usuario.</param>
public class Status(string nuevoEstado)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.STATUS;

	/// <summary>
	/// status es el nuevo estado del usuario.
	/// </summary>
	public string status { get; set; } = nuevoEstado;

	public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}