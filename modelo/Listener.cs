using System.Net;
using System.Net.Sockets;
namespace chat.modelo;

public class Listener
{
	public int puerto { get; private set; } = 1111;
	public bool Escuchando { get; private set; }
	public Socket listener { get; set; }
	IPAddress ipServidor { get; set; } = IPAddress.Parse("127.0.0.1");
	public Listener(int _puerto)
	{
		puerto = _puerto;
		listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}
	public void Escuchar()
	{
		if (Escuchando)
			return;
		listener.Bind(new IPEndPoint(0, puerto));
		listener.Listen(100); //máximo 100 peticiones en la cola plz
		listener.BeginAccept(callback, null);
		Escuchando = true;
	}
	public void Detener()
	{
		if (!Escuchando)
			return;
		listener.Close();
		listener.Dispose();
		listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}
	void callback(IAsyncResult asyncResult)
	{
		try
		{
			Socket socket = this.listener.EndAccept(asyncResult);
			if (SocketAccepted is not null)
			{
				SocketAccepted(socket);
			}
			this.listener.BeginAccept(callback, null);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
	}
	public delegate void SocketAcceptedHandler(Socket e);
	public event SocketAcceptedHandler SocketAccepted;
}