using System.Text.Json.Serialization;
namespace chat.modelo.protocolo;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum resultOptions
{
	SUCCESS,
	USER_ALREADY_EXISTS,
	NO_SUCH_USER,
	ROOM_ALREADY_EXISTS,
	NO_SUCH_ROOM,
	NOT_INVITED,
	NOT_JOINED,
	NOT_IDENTIFIED,
	INVALID
}