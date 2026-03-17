using System.Text.Json.Serialization;
namespace chat.modelo.protocolo;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum typeOptions
{
	IDENTIFY,
	RESPONSE,
	NEW_USER,
	STATUS,
	USERS,
	USER_LIST,
	TEXT,
	TEXT_FROM,
	PUBLIC_TEXT,
	PUBLIC_TEXT_FROM,
	NEW_ROOM,
	INVITE,
	INVITATION,
	JOIN_ROOM,
	JOINED_ROOM,
	ROOM_USERS,
	ROOM_USER_LIST,
	LEAVE_ROOM,
	LEFT_ROOM,
	DISCONNECT,
	DISCONNECTED
} 