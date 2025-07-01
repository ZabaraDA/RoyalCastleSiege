using Newtonsoft.Json;
using UnityEngine;

public class EnemyTypeData
{
    [JsonProperty("id")]
    public int Id;
    [JsonProperty("healt")]
    public int Healt;
    [JsonProperty("speed")]
    public int Speed;
    [JsonProperty("damage")]
    public int Damage;
    [JsonProperty("prize")]
    public int Prize;
}
