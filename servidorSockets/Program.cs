using System.Net;
using System.Net.Sockets;
using System.Text;
using chat.modelo;
//Argumento 1: puerto para abrir.
//Argumento 2: máximo de clientes #TO-DO
int puerto = 1111;
TcpListener servidor;
inicializar();

while (true)
{
    TcpClient cliente = await servidor.AcceptTcpClientAsync();
    _ = ManejarConexionAsincrona(cliente); 
}


/*--------------métodos!-------------*/
bool inicializar()
{
	if(args.Length>0)
	//si es un entero válido, lo asignamos
	if (!int.TryParse(args[0], out puerto))
	{
		Console.WriteLine("El argumento " + args[0] + " proporcionado como" +
		" primer argumento no es un entero. Se usará el puerto default 1111");
		puerto = 1111; //sin esto, el tryparse le plancha un 0 al puerto.
	}
	Console.WriteLine("Inicializando Servidor de chat en el puerto " +
	  puerto.ToString());
	servidor = new TcpListener(IPAddress.Any, puerto);
	servidor.Start();
	return true;
}

async Task ManejarConexionAsincrona(TcpClient cliente)
{
    using (cliente)
	{
		int longitudMensaje;
		Socket aceptado = cliente.Client;
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"Cliente conectado! {aceptado.RemoteEndPoint}"+ 
	      $"a las {DateTime.Now}.");
        var stream = cliente.GetStream();
        byte[] buffer = new byte[2048];
		try
		{
			while (true)
			{
				longitudMensaje = await stream.ReadAsync(buffer, 0, buffer.Length);
				if (longitudMensaje <= 0)
				{
					break; // Cliente desconectado, hay que morir
				}
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"[{aceptado.RemoteEndPoint}]:" +
				 $"{Encoding.UTF8.GetString(buffer, 0, longitudMensaje)}");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error en la conexión: {aceptado.RemoteEndPoint}: {e.Message}");
		}
		Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Cliente desconectado! {aceptado.RemoteEndPoint}");
    }
}