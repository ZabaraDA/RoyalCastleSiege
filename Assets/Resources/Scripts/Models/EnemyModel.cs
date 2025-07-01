using System;
using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public int Number { get; set; }
    private int _healts;

    public event Action<int> OnModelHealtsChanged;

    public int Healts 
    {
        get
        {
            return _healts;
        }
        set
        {
            if (_healts != value)
            {
                _healts = value;
                OnModelHealtsChanged?.Invoke(_healts);
                Debug.Log($"Field '{nameof(Healts)}' changed in model");
            }
        }
    }

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
    }

    public void TakeDamage(int damage)
    {
        Healts -= damage;
    }
}
