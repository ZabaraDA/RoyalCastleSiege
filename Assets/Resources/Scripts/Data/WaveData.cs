using Newtonsoft.Json;
using System.Collections.Generic;

public class WaveData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("enemys")]
    public List<EnemyCount> EnemyCountList;
}

public class EnemyCount
{
    [JsonProperty("enemyid")]
    public int EnemyTypeId;
    [JsonProperty("count")]
    public int Count;
}
