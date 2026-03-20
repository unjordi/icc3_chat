using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using chat.modelo.protocolo;

//Argumento 1: puerto para conectarse.
//Argumento 2: ip del servidor
TcpClient servidor = new();
NetworkStream stream = null;

int puerto = 1111;
IPAddress ipServidor = IPAddress.Parse("127.0.0.1");
string mensajeCrudo = "";

if (await inicializar())
{
	stream = servidor.GetStream();
	_ = Task.Run(async () =>
		{
			byte[] bufferRecepcion = new byte[2048];
			try
			{
				int i = 0;
				while (true)
				{
					// This stays waiting for the server, even if the user is typing
					int bytesRecibidos = await stream.ReadAsync(bufferRecepcion, 0, bufferRecepcion.Length);
					if (bytesRecibidos == 0) break;
					if (i == 0)
					{
						Response respuesta = JsonSerializer.Deserialize<Response>
						(Encoding.UTF8.GetString(bufferRecepcion, 0, bytesRecibidos));
						if(respuesta.operation == operationOptions.IDENTIFY && respuesta.result == resultOptions.SUCCESS)
						{
							Console.WriteLine($"\r\t|Bienvenido al chat, {respuesta.extra}");
						}
					}
					else
					{
						Response respuesta = JsonSerializer.Deserialize<Response>
						(Encoding.UTF8.GetString(bufferRecepcion, 0, bytesRecibidos));
						Console.Write("Ingrese el texto a enviar: ");
					}
					i++;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("\nConexión perdida con el servidor: " + e.Message);
			}
		});
		while (!(mensajeCrudo is null ? "" : mensajeCrudo).Equals("salir"))
		{
			try
			{
				Console.WriteLine("Ingrese el texto a enviar.");
				mensajeCrudo = Console.ReadLine();
				byte[] mensajeJson = Encoding.UTF8.GetBytes(mensajeCrudo is null ? "" : mensajeCrudo);
				await stream.WriteAsync(mensajeJson);
			}
			catch (Exception e)
			{
				Console.WriteLine("\nConexión perdida con el servidor." + e.Message);
				break;
			}
		}
}

Console.Read();

async Task<bool> inicializar()
	{
	if (args.Length == 2)
	{
		if (!IPAddress.TryParse(args[0], out ipServidor))
		{
			Console.WriteLine("El argumento " + args[0] + " proporcionado como" +
			" primer argumento no es una ip. Se usará la ip default 127.0.0.1");
			ipServidor = IPAddress.Parse("127.0.0.1");
		}
		//si es un entero válido, lo asignamos
		if (!int.TryParse(args[1], out puerto))
		{
			Console.WriteLine("El argumento " + args[1] + " proporcionado como" +
			" segundo argumento no es un entero. Se usará el puerto default 1111");
			puerto = 1111; //sin esto, el tryparse le plancha un 0 al puerto.
		}
	}else if (args.Length > 0)
	{
		Console.WriteLine($"Este programa requiere 2 argumentos:{Environment.NewLine}" 
		+$"una ip válida que apunte al servidor {Environment.NewLine}"
		+$"el puerto que el servidor haya abierto para el cliente. {Environment.NewLine}"
		+$"Si no se proporcionan los dos o no se indican datos válidos, {Environment.NewLine}"
		+$"se usarán la ip default 127.0.0.1 y el puerto default 1111. {Environment.NewLine}");
	}
	Console.WriteLine("Inicializando chat con el servidor "
	+ ipServidor.ToString() + " en el puerto " + puerto.ToString());
	System.Console.Write("Ingrese el username deseado: ");
	string username = Console.ReadLine();
	try
	{
		await servidor.ConnectAsync(ipServidor, puerto);
		stream = servidor.GetStream();
		Identify identify = new(username);
		byte[] mensajeJson = Encoding.UTF8.GetBytes(username is null ? "" : identify.ToString());
		await stream.WriteAsync(mensajeJson);
		return true;
	}
	catch
	{
		System.Console.WriteLine($"no se logró establecer la conexión al "
		+ $"servidor.{Environment.NewLine} El programa terminará. Revise los "
		+ "parámetros y confirme que el servidor se esté ejecutando en la misma red.");
		return false;
	}
}