using System;
using UnityEngine;

public class UnitStatsController : MonoBehaviour
{
    public event Action<float> OnRateChanged;
    public event Action<float> OnSpeedChanged;

    public float Speed { get => (defaultSpeed + Mathf.Max(0, bonusTermSpeed)) * Mathf.Max(0, bonusFactorSpeed); }
    public float AngularSpeed { get => defaultAngularSpeed; }

    public float AttackRate { get => (defaultAttackRate + Mathf.Max(0, bonusTermAttackRate)) * Mathf.Max(0,bonusFactorAttackRate); }
    public int AttackDamage { get => defaultAttackDamage; }
    public float Range { get => defaultRange; }


    private float defaultSpeed = 1;
    private float bonusTermSpeed;
    private float bonusFactorSpeed = 1;

    private float defaultAngularSpeed;

    private float defaultAttackRate = 1;
    private float bonusTermAttackRate;
    private float bonusFactorAttackRate = 1;

    private int defaultAttackDamage;

    private float defaultRange;

    public void Initialize(UnitData unitData)
    {
        SetDefaultSpeed(unitData.Speed);
        SetDefaultAttackRate(unitData.AttackRate);
        SetDefaultAngularSpeed(unitData.AngularSpeed);
        SetDefaultAttackDamage( unitData.AttackDamage);
        SetDefaultRange(unitData.Range);
    }

    #region Speed

    public void SetDefaultSpeed(float speed)
    {
        defaultSpeed = speed;

        OnSpeedChanged?.Invoke(Speed);
    }

    public void AddBonusTermSpeed(float term)
    {
        bonusTermSpeed += term;

        OnSpeedChanged?.Invoke(Speed);
    }

    /// <summary>
    /// from 1, giving factor = 0.3 => 1.3
    /// </summary>
    /// <param name="factor"></param>
    public void AddBonusFactorSpeed(float factor)
    {
        bonusFactorSpeed += factor;

        OnSpeedChanged?.Invoke(Speed);
    }


    public void SetDefaultAngularSpeed(float angularSpeed)
    {
        defaultAngularSpeed = angularSpeed;
    }

    #endregion

    #region Attack

    public void SetDefaultAttackRate(float rate)
    {
        defaultAttackRate = rate;

        OnRateChanged?.Invoke(AttackRate);
    }


    public void SetDefaultAttackDamage(int damage)
    {
        defaultAttackDamage = damage;
    }

    public void SetDefaultRange(float range)
    {
        defaultRange = range;
    }

    #endregion
}
