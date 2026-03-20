using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using chat.modelo.protocolo;
using modelo;
//Argumento 1: puerto para abrir.
//Argumento 2: máximo de clientes #TO-DO
int puerto = 1111;
TcpListener servidor;
List<Usuario> usuarios = new();
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
		var stream = cliente.GetStream();
		byte[] buffer = new byte[2048];
		int i = 0;
		try
		{
			while (true)
			{
				longitudMensaje = await stream.ReadAsync(buffer, 0, buffer.Length);
				if (longitudMensaje <= 0)
				{
					break; // Cliente desconectado, hay que morir
				}
				if (i == 0)
				{
					if (!await RecibirUsuarioNuevoAsync(cliente, buffer, longitudMensaje)) break;
				}
				else
				{
					Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, longitudMensaje));
				}
				i++;
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
async Task<bool> RecibirUsuarioNuevoAsync(TcpClient clientenuevo, byte[] buffer,int longitudMensaje)
{
	Socket aceptado = clientenuevo.Client;
		var stream = clientenuevo.GetStream();
	Identify saludo = JsonSerializer.Deserialize<Identify>
		(Encoding.UTF8.GetString(buffer, 0, longitudMensaje));
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine($"Se conectó {saludo.username} desde [{aceptado.RemoteEndPoint}]" +
			$"a las {DateTime.Now}.");
	usuarios.Add(new(saludo.username, clientenuevo));
	Response respuesta = new(operationOptions.IDENTIFY, resultOptions.SUCCESS, saludo.username);
	byte[] responseBytes = Encoding.UTF8.GetBytes(respuesta.ToString());
	await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
	Console.ForegroundColor = ConsoleColor.White;
	return true;
}