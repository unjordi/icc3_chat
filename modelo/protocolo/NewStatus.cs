using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de NEW_STATUS.
/// </summary>
/// <remarks>
/// Constructor de mensaje NEW_STATUS.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
/// <param name="estado">El nuevo STATUS del usuario.</param>
public class NewStatus(string username,string estado)
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.NEW_STATUS;
	/// <summary>
	/// username es el nombre de usuario.
	/// </summary>
	public string username { get; set; } = username;

	/// <summary>
    /// Estado es el nuevo estado del usuario.
    /// </summary>
    public String status { get; set; } = estado;
	public override string ToString()
	{
		return JsonSerializer.Serialize(this);
	}
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}