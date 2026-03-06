using System.Net;
using System.Net.Sockets;
namespace chat.modelo;

public class Cliente
{
	public string id { get; private set; }
	public string username { get;  set; }
	public IPEndPoint EndPoint { get; set; }
	public Socket enchufe { get; }
	public Cliente(Socket aceptado)
	{
		enchufe = aceptado;
		id = Guid.NewGuid().ToString();
		EndPoint = (IPEndPoint)enchufe.RemoteEndPoint;
		enchufe.BeginReceive(new byte[]{ 0 },0,0,0,callback,null);
	}
	void callback(IAsyncResult asyncResult)
	{
		try
		{
			enchufe.EndReceive(asyncResult);
			byte[] buffer = new byte[8192];
			int longitudMensaje = enchufe.Receive(buffer, buffer.Length, 0);
			if (longitudMensaje < buffer.Length)
			{
				Array.Resize<byte>(ref buffer,longitudMensaje);
			}
			if (Recibido != null)
			{
				Recibido(this, buffer);
			}
			enchufe.BeginReceive(new byte[]{ 0 },0,0,0,callback,null);
		}
		catch(Exception e)
		{
			Console.WriteLine(e.Message);
			Cerrar();
			if(Desconectado != null)
			{
				Desconectado(this);
			}
		}
	}
	public void Cerrar()
	{
		enchufe.Close();
		enchufe.Dispose();
	}
	public delegate void MensajeRecibidoManejador(Cliente remitente, byte[] datos);
	public delegate void ClienteDesconectadoManejador(Cliente remitente);

	public event MensajeRecibidoManejador Recibido;
	public event ClienteDesconectadoManejador Desconectado;
}