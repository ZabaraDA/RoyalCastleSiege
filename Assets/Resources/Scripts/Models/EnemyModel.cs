using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public int Number { get; set; }
    public int Healt { get; set; }
    public IEnemyTypeModel Type { get; set; }
}
