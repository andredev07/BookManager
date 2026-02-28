using System.Text.Json.Serialization;

namespace BookstoreManager.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookGenre
    {
        Ficcao,
        Romance,
        Misterio
    }
}
