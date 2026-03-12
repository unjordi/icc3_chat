using System.Net;
using System.Net.Sockets;
namespace chat.modelo;

public class Conexion
{
	public int puerto { get; private set; } = 1111;
	public bool Escuchando { get; private set; }
	public Socket listener { get; set; }
	public Conexion(int _puerto)
	{
		puerto = _puerto;
		Escuchando = false;
		listener = new(AddressFamily.InterNetwork, SocketType.Stream,
		  ProtocolType.Tcp);
	}
	public void Escuchar()
	{
		try
		{
		if (Escuchando)
			return;
		listener.Bind(new IPEndPoint(0, puerto));
		listener.Listen(100); //máximo 100 peticiones en la cola plz
		listener.BeginAccept(callback, null);
		Escuchando = true;
		}
		catch (Exception e)
		{
			Console.WriteLine($"Hubo un error al inicializar el puerto:"+ 
			$"{Environment.NewLine}==>{e.Message}");
			return;
		}
	}

	public void Detener()
	{
		if (!Escuchando)
			return;
		listener.Close();
		listener.Dispose();
		listener = new(AddressFamily.InterNetwork, SocketType.Stream,
		  ProtocolType.Tcp);
	}
	void callback(IAsyncResult asyncResult)
	{
		try
		{
			Socket socket = this.listener.EndAccept(asyncResult);
			if (ConexionAceptada is not null)
			{
				ConexionAceptada(socket);
			}
			this.listener.BeginAccept(callback, null);
		}
		catch (Exception e)
		{
			Console.WriteLine($"Hubo un error al recibir la conexión:"+ 
			$"{Environment.NewLine}==>{e.Message}");
		}
	}
	public delegate void ManejadordeConexionAceptada(Socket e);
	public event ManejadordeConexionAceptada ConexionAceptada;
}