using System.Text.Json;
using System.Text.Json.Serialization;
namespace chat.modelo.protocolo;

/// <summary>
/// Esta clase representa un mensaje de RESPONSE.
/// </summary>
public class Response
{
    /// <summary>
    /// type es el tipo de mensaje.
    /// </summary>
    public typeOptions type {get;} = typeOptions.RESPONSE;
    
    /// <summary>
    /// operation es el tipo de mensaje al que se está respondiendo.
    /// </summary>
    public operationOptions operation {get;}

    /// <summary>
    /// result representa el éxito o fracaso.
    /// </summary>
    public resultOptions result {get;}

    /// <summary>
    /// extra es el contenido adicional.
    /// </summary>
    public string extra {get;set;}

    /// <summary>
    /// Constructor de mensaje RESPONSE.
    /// </summary>
    /// <param name="username">El nombre de usuario.</param>
    public Response(operationOptions operation, resultOptions result,string extra)
    {
        this.operation = operation;
        this.result = result;
        this.extra  = extra;
    }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    public string toJson()
    {
        return JsonSerializer.Serialize(this);
    }
}