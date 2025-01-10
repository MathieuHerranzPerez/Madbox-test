using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Gameplay/UnitData")]
public class UnitData : ScriptableObject
{
    [field: SerializeField] public Unit UnitPrefab { get; private set; }

    [Header("Health")]
    public int MaxHealth = 100;
    [Tooltip("Time to live after death (to play anim, fx, sounds, whatever...)")]
    public float DeathAnimDuration = 2.5f;

    [Header("Movement")]
    [Tooltip("Unit per sec")]
    public float Speed = 3;

    [Tooltip("Angular speed per sec")]
    public float AngularSpeed = 1800f;

    [Header("Attack")]
    public float AttackRate = 1f;
    public int AttackDamage = 20;
    public float Range = 2f;
    public UnitTargeterStrategy UnitTargeterStrategy;
}
