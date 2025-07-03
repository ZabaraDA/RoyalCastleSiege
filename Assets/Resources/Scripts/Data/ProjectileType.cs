using Newtonsoft.Json;
using UnityEngine;

public class ProjectileTypeData
{
    [JsonProperty("id")]
    public int Id;
    [JsonProperty("damage")]
    public int Damage;
    [JsonProperty("speed")]
    public int Speed;
    [JsonProperty("cost")]
    public int Cost; 
}
