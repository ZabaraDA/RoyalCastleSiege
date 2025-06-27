using UnityEngine;

public interface IEnemyModel
{
    int Number { get; set; }
    int Healt { get; set; }
    IEnemyTypeModel Type { get; set; }
}
