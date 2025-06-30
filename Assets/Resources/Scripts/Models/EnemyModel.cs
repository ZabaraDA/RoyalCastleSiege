using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public int Number { get; set; }
    public int Healts { get; set; }
    public IEnemyTypeModel Type { get; set; }

    public bool IsAlive => Healts > 0;

    public Vector2 Position { get; set; }
    public Vector2 TargetPosition { get; set; }

    public EnemyModel(int number, Vector2 position, Vector2 direction, IEnemyTypeModel type)
    {
        Number = number;
        Type = type;
        Position = position;
        TargetPosition = direction;
        Healts = type.Healt;
    }

    public void UpdatePosition(float deltaTime)
    {
        //ToDo
        Vector2 directon = (TargetPosition - Position).normalized;
        Position += deltaTime * Type.Speed * directon;
        Debug.Log(Position);
    }
}
