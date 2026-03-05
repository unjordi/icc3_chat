using System.Net;
using System.Net.Sockets;
using System.Text;
//Argumento 1: puerto para conectarse.
//Argumento 2: ip del servidor
Socket socket;
int puerto = 1111;
IPAddress? ipServidor = IPAddress.Parse("127.0.0.1");
String? mensajeCrudo = "";
if(!inicializar())
{
	return;
}
while (!(mensajeCrudo is null ? "" : mensajeCrudo).Equals("salir"))
{
	System.Console.WriteLine("Ingrese el texto a enviar.");
	mensajeCrudo = Console.ReadLine();
	byte[] mensajeJson = Encoding.UTF8.GetBytes(mensajeCrudo is null ? "" : mensajeCrudo);

	await socket.SendAsync(mensajeJson);

}
Console.Read();
socket.Close();


bool inicializar()
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
	+ipServidor.ToString()+" en el puerto "+puerto.ToString());
	socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	IPEndPoint servidor = new (ipServidor, puerto);
	try
	{
		socket.Connect(servidor); 
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