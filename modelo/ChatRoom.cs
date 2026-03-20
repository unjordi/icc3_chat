using System.Net;
using System.Net.Sockets;
namespace chat.modelo;

public class ChatRoom
{
	/// <summary>
    /// NombreCuarto es el nombre del ChatRoom
    /// </summary>
    public string NombreCuarto { get; set; }

	/// <summary>
	/// Miembros es la lista de Usuarios del ChatRoom
	/// </summary>
	public List<Usuario> Miembros { get; set; } = new();
	
	/// <summary>
    /// Invitados es la lista de Usuarios que pueden entrar al ChatRoom
    /// </summary>
	public HashSet<string> Invitados { get; set; } = new();
	
	/// <summary>
    /// Constructor de ChatRoom.
    /// </summary>
    /// <param name="cuarto">El nombre del cuarto.</param>
	/// <param name="creador">El nombre de usuario creador</param>
    public ChatRoom(string nombre, Usuario creador)
    {
        NombreCuarto = nombre;
        Miembros.Add(creador);
    }
}