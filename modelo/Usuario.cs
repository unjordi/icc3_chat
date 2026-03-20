using System.Net.Sockets;

namespace chat.modelo;

/// <summary>
/// Esta clase representa un usuario y sus acciones.
/// </summary>
/// <remarks>
/// Constructor de usuarios.
/// </remarks>
/// <param name="username">El nombre de usuario.</param>
/// <param name="conexion">socket para conexion con el usuario</param>
public class Usuario(String username, TcpClient sock)
{
	/// <summary>
	/// username es el nombre de usuario.
	/// </summary>
	public String Username { get; set; } = username;

	/// <summary>
	/// Estado es el STATUS del usuario.
	/// </summary>
	public String Estado { get; set; } = "ACTIVE"; //Todos los usuarios nacen vivos

	/// <summary>
	/// conexion contiene el socket con el que el 
	/// servidor se comunica con el usuario.
	/// </summary>
	public TcpClient Conexion { get; set; } = sock;
}
