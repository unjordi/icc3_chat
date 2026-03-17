using System.Text.Json.Serialization;
namespace chat.modelo.protocolo;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum operationOptions
{
	IDENTIFY,
	TEXT,
	NEW_ROOM,
	INVITE,
	JOIN_ROOM,
	ROOM_USERS,
	ROOM_TEXT,
	LEAVE_ROOM,
	INVALID
} 