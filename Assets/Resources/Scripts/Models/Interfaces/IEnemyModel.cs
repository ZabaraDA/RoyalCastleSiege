using UnityEngine;

public interface IEnemyModel
{
    int Number { get; set; }
    int Healts { get; set; }
    IEnemyTypeModel Type { get; set; }
    bool IsAlive { get; }
    Vector2 Position { get; set; }
    Vector2 TargetPosition { get; set; }

    void UpdatePosition(float deltaTime);
}
