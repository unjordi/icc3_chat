using System.Net.Sockets;

namespace chat.modelo;

/// <summary>
/// Esta clase representa un usuario y sus acciones.
/// </summary>
public class Usuario
{ 
    /// <summary>
    /// GUID es el id del usuario.
    /// </summary>
    public Guid GUID { get; private set; }

	/// <summary>
	/// username es el nombre de usuario.
	/// </summary>
	public String Username { get; set; } = "";
	
	/// <summary>
    /// username es el nombre de usuario.
    /// </summary>
    public String Estado { get; set; } = "";
    
    /// <summary>
    /// conexion contiene el socket con el que el 
    /// servidor se comunica con el usuario.
    /// </summary>
    public TcpClient Conexion { get; set; } 

    /// <summary>
    /// Constructor de usuarios.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    /// <param name="conexion">socket para conexion con el usuario</param>
    public Usuario(String username,TcpClient sock )
    {
		Username = username;
		Estado = "ACTIVE"; //Todos los usuarios nacen vivos
        Conexion = sock;
        GUID = Guid.NewGuid();
    }
   
}
