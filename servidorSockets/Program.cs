
using System.Net;
using System.Net.Sockets;
using System.Text;
//Argumento 1: puerto para abrir.
//Argumento 2: máximo de clientes #TO-DO
Socket listener, aceptado;
byte[] buffer;



inicializar();
recibirCliente();

Console.Read();
listener.Close();

/*--------------métodos!-------------*/
bool inicializar()
{
	listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	aceptado = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	int puerto = 1111;
	IPEndPoint local = new(0, puerto);
	listener.Bind(local);
	listener.Listen(100); //máximo 100 peticiones en cola plz.

	if(args.Length>0)
	//si es un entero válido, lo asignamos
	if (!int.TryParse(args[0], out puerto))
	{
		Console.WriteLine("El argumento " + args[0] + " proporcionado como" +
		" primer argumento no es un entero. Se usará el puerto default 1111");
		puerto = 1111; //sin esto, el tryparse le plancha un 0 al puerto.
	}
Console.WriteLine("Inicializando Servidor de chat en el puerto "+puerto.ToString());
	return true;
}

void recibirCliente()
{
	new Thread(delegate ()
	{
		aceptado = listener.Accept();
		System.Console.WriteLine("cliente conectado!");
		listener.Close();

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
	).Start();
	aceptado.Close(); 
	return ;
}