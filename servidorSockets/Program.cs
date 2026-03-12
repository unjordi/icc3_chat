using System.Net;
using System.Net.Sockets;
using System.Text;
using chat.modelo;
//Argumento 1: puerto para abrir.
//Argumento 2: máximo de clientes #TO-DO
Conexion conexion;
byte[] buffer;
int puerto = 1111;
inicializar();



Console.Read();
//aceptado.Dispose();

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
	Console.WriteLine("Inicializando Servidor de chat en el puerto "+
	  puerto.ToString());
	conexion = new(puerto);
	conexion.ConexionAceptada += 
	  new Conexion.ManejadordeConexionAceptada(conexion_ConexionAceptada);
	conexion.Escuchar();
	return true;
}

void conexion_ConexionAceptada(Socket aceptado)
{
	Console.WriteLine($"Cliente conectado! {aceptado.RemoteEndPoint}"+ 
	  $"a las {DateTime.Now}.");
	while (true)
		{
			try
			{
				buffer = new byte[aceptado.SendBufferSize];
				int longitudMensaje = aceptado.Receive(buffer);
				if (longitudMensaje <= 0)
				{
					throw new SocketException();
				}
				//pasamos el mensaje en limpio
				Array.Resize(ref buffer, longitudMensaje);
				//y directo a consola, por ahora 
				Console.WriteLine(Encoding.UTF8.GetString(buffer));
			}
			catch
			{
				System.Console.WriteLine("Cliente desconectado.");
				return;
			}
		}
}