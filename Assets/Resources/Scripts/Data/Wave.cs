using Newtonsoft.Json;
using System.Collections.Generic;

public class Wave
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("enemys")]
    public List<EnemyCount> EnemyCountList;
}

public class EnemyCount
{
    [JsonProperty("enemyid")]
    public int EnemyId;
    [JsonProperty("count")]
    public int Count;
}
