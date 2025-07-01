using UnityEngine;

public class EnemyTypeModel : IEnemyTypeModel
{
    public int Id { get; set; }
    public int Healt { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
    public int Prize { get; set; }
}
