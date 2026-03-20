using System.Text.Json;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un avisod de ingreso de usuario al chat.
/// </summary>
public class UserList
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions Type {get;} = typeOptions.USER_LIST;
    
    /// <summary>
    /// username es el nombre de usuario.
    /// </summary>
    public Dictionary<string, string> Usuarios { get; set; } = new();
    
    /// <summary>
    /// Constructor de mensaje UserList.
    /// </summary>
    /// <param name="Usuarios">El nombre de usuario.</param>
    public UserList(List<Usuario> usuarios)
	{
		foreach(var usuario in usuarios)
		{
			Usuarios.Add(usuario.Username, usuario.Estado);
		}
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