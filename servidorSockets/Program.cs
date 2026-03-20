using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using chat.modelo.protocolo;
using chat.modelo;
//Argumento 1: puerto para abrir.
//Argumento 2: máximo de clientes #TO-DO
int puerto = 1111;
TcpListener servidor;
Lock _lock = new();
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
	Usuario noob = null;
	using (cliente)
	{
		int longitudMensaje;
		Socket aceptado = cliente.Client;
		var stream = cliente.GetStream();
		byte[] buffer = new byte[2048];
		bool loggeado = false;
		try
		{
			while (true)
			{
				longitudMensaje = await stream.ReadAsync(buffer, 0, buffer.Length);
				if (longitudMensaje <= 0)
				{
					break; // Cliente desconectado, hay que morir
				}
				if (!loggeado)
				{
					noob = await RecibirUsuarioNuevoAsync(cliente, buffer, longitudMensaje);
					if (noob is null)
					{
						var error = new Response(operationOptions.INVALID, resultOptions.NOT_IDENTIFIED, "");
						byte[] errorBytes = Encoding.UTF8.GetBytes(error.toJson());
						await stream.WriteAsync(errorBytes);
						break;
					}
					lock (_lock) { usuarios.Add(noob); }
					loggeado = true;
				}
				else
				{
					Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, longitudMensaje));
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error en la conexión: {aceptado.RemoteEndPoint}: {e.Message}");
		}
		finally
		{
			if (noob != null)
			{
				lock (_lock)
				{
					Usuario moribundo = usuarios.FirstOrDefault(u => u.Username == noob.Username);
					if (moribundo != null) usuarios.Remove(moribundo);
				}
				await Broadcast($"{noob.Username} ha salido del chat.", "SISTEMA");
			}
		}
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"Cliente desconectado! {aceptado.RemoteEndPoint}");
	}
}
async Task<Usuario> RecibirUsuarioNuevoAsync(TcpClient clientenuevo, byte[] buffer,int longitudMensaje)
{
	Usuario novato = null;
	try
	{
		Socket aceptado = clientenuevo.Client;
		var stream = clientenuevo.GetStream();
		Identify saludo = JsonSerializer.Deserialize<Identify>
			(Encoding.UTF8.GetString(buffer, 0, longitudMensaje));
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"Se conectó {saludo.username} desde [{aceptado.RemoteEndPoint}]" +
				$"a las {DateTime.Now}.");
		novato = new(saludo.username, clientenuevo);
		Response respuesta = new(operationOptions.IDENTIFY, resultOptions.SUCCESS, saludo.username);
		byte[] responseBytes = Encoding.UTF8.GetBytes(respuesta.ToString());
		await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
	}
	catch (Exception e)
	{
		Console.WriteLine($"Error al saludar al usuario: {novato.Username}: {e.Message}");
	}
	return novato;
}

async Task Broadcast(string message, string senderName)
{
    byte[] data = Encoding.UTF8.GetBytes($"{senderName}: {message}");
    
    // Create a copy of the list to avoid "Collection Modified" errors 
    // if someone disconnects while we are looping.
    List<Usuario> copiaUsuarios;
    lock (_lock)
    {
        copiaUsuarios = usuarios.ToList();
    }

    foreach (var usuario in copiaUsuarios)
    {
        try
        {
            var stream = usuario.Conexion.GetStream();
            await stream.WriteAsync(data, 0, data.Length);
        }
        catch
        {
            Console.WriteLine($"Falló el envío a {usuario.Username}");
        }
    }
}