namespace modelo;

/// <summary>
/// Esta clase representa un usuario y sus acciones.
/// </summary>
public class Usuario
{ 
    /// <summary>
    /// Constructor de usuarios.
    /// </summary>
    /// <param name="nick">El nombre de usuario.</param>
    public Usuario(String nick)
    {
        this.Nick = nick;
    }
    /// <summary>
    /// Nick es el nombre de usuario.
    /// </summary>
    public String Nick { get; set; } = "";
}
