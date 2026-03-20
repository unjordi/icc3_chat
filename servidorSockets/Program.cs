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
					string JsonCrudo = Encoding.UTF8.GetString(buffer, 0, longitudMensaje);
					try 
					{
						using JsonDocument jsonSinTipo = JsonDocument.Parse(JsonCrudo);
						string tipoMensaje = jsonSinTipo.RootElement.GetProperty("type").GetString();

						switch (tipoMensaje)
						{
							case "PUBLIC_TEXT":
								PublicTextFrom mensajePublico = new(noob.Username, jsonSinTipo.RootElement.GetProperty("text").GetString());
								await Pregonar(noob.Username,mensajePublico.ToString());
								break;
							case "STATUS":
								string nuevoEstado = jsonSinTipo.RootElement.GetProperty("status").GetString() ?? "";
								await ManejarCambioEstado(noob, nuevoEstado);
								break;
							case "DISCONNECT":
								await Desconectar(noob);
								break;
							case "USERS":
								await EnviarListaUsuarios(noob);
								break;
							default:
								// si no sé qué hacer, me muero.
								System.Console.WriteLine("tipo de mensaje fuera de protocolo, abortando.");
								await EnviarRespuestaInvalida(noob);
								throw new Exception("Tipo de mensaje no reconocido: "+JsonCrudo);
						}
					}
					catch (Exception ew)
					{
						System.Console.WriteLine(ew.Message);
						var error = new Response(operationOptions.INVALID, resultOptions.INVALID, "");
						await stream.WriteAsync(Encoding.UTF8.GetBytes(error.toJson()));
						continue; // Le da otra oportunidad!
					}
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
				await Desconectar(noob);
			}
		}
		
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
		await Pregonar(novato.Username,new NewUser(novato.Username).ToString());
	}
	catch (Exception e)
	{
		Console.WriteLine($"Error al saludar al usuario: {novato.Username}: {e.Message}");
	}
	return novato;
}

async Task Pregonar(string remitente,string mensajeJson)
{
	byte[] data = Encoding.UTF8.GetBytes($"{mensajeJson}");
	if (remitente.Equals("SISTEMA"))
	{
		Console.CursorLeft = Console.BufferWidth - (int)("[{remitente}]> {mensajeJson}".Length*2.5);
		Console.WriteLine($"[{remitente}]> {mensajeJson}");
	}
	else
	{
		Console.WriteLine($"[{remitente}]> {mensajeJson}");
	}
	
	// dice el internet (la documentación) que si no itero sobre una copia,
	// me cae la policía de la concurrencia y pasan cosas malas.
	// y que para sacar la copia tengo que prevenir las condiciones de carrera.
	List<Usuario> copiaUsuarios;
	lock (_lock)
	{
		copiaUsuarios = usuarios.ToList();
	}
	foreach (var usuario in copiaUsuarios)
	{
		if(usuario.Username != remitente)
		try
		{
			var stream = usuario.Conexion.GetStream();
			await stream.WriteAsync(data, 0, data.Length);
		}
		catch
		{
			Console.WriteLine($"Falló el envío de mensaje a {usuario.Username}");
		}
	}
}

/// <summary>
/// Cambia el estado de un usuario y notifica a los demás.
/// </summary>
/// <param name="usuario">El usuario que cambia de estado.</param>
/// <param name="estado">El nuevo estado.</param>
async Task ManejarCambioEstado(Usuario usuario, string estado)
{
	if (estado != "ACTIVE" && estado != "AWAY" && estado != "BUSY")
    {
		await EnviarRespuestaInvalida(usuario);
    }
    usuario.Estado = estado;
	// Crear el mensaje NEW_STATUS para los demás
	NewStatus newStatus = new(usuario.Username, estado);
    // Broadcast a todos menos al interesado
    await Pregonar(usuario.Username, newStatus.ToString());
}

/// <summary>
/// Envía la lista de usuarios conectados al usuario solicitante.
/// </summary>
/// <param name="solicitante">El usuario que pidió la lista.</param>
async Task EnviarListaUsuarios(Usuario solicitante)
{
	List<Usuario> listaActual;
	lock (_lock)
	{
		listaActual = usuarios.ToList();
	}
	UserList respuesta = new UserList(listaActual);
	byte[] data = Encoding.UTF8.GetBytes(respuesta.ToString());
	await solicitante.Conexion.GetStream().WriteAsync(data);
}

/// <summary>
/// Envía respuesta INVALID al usuario solicitante.
/// </summary>
/// <param name="solicitante">El usuario al que se responderá.</param>
async Task EnviarRespuestaInvalida(Usuario solicitante)
{
	Response invalida = new(operationOptions.INVALID, resultOptions.INVALID, "");
	byte[] data = Encoding.UTF8.GetBytes(invalida.ToString());
	await solicitante.Conexion.GetStream().WriteAsync(data);
	await Desconectar(solicitante);
}

/// <summary>
/// Desconecta al usuario.
/// </summary>
/// <param name="solicitante">El usuario que se desconectó.</param>
async Task Desconectar(Usuario solicitante)
{
	Disconected desconexion = new(solicitante.Username);
    byte[] data = Encoding.UTF8.GetBytes(desconexion.ToString());
	await solicitante.Conexion.GetStream().WriteAsync(data);
	lock (_lock)
	{
		Usuario moribundo = usuarios.FirstOrDefault(u => u.Username == solicitante.Username);
		if (moribundo != null) usuarios.Remove(moribundo);
	}
	await Pregonar("SISTEMA",desconexion.ToString());
	Console.ForegroundColor = ConsoleColor.Red;
	Console.WriteLine($"Cliente desconectado! {solicitante.Username}");
	Console.ForegroundColor = ConsoleColor.Green; 
}