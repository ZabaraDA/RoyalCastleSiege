using UnityEngine;

public interface IEnemyTypeModel
{
    int Id { get; set; }
    int Healt { get; set; }
    int Speed { get; set; }
    int Damage { get; set; }
    int Prize { get; set; }

    Sprite Sprite { get; set; }
}
