using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region events
    public event Action OnDeath;
    /// <summary>
    /// int = previousValue
    /// int = currentValue
    /// </summary>
    public event Action<int, int> OnDamageTaken;
    public event Action OnInitialized;
    #endregion


    [field: SerializeField] public UnitData UnitData { get; private set; }
    public bool IsInitialized { get; private set; } = false;
    public bool IsAlive { get; private set; }
    public float Radius => unitCollider.radius;
    public int CurrentHealth => health.Current;
    public int MaxHealth => health.Max;


    [SerializeField] private SphereCollider unitCollider;


    private CounterWithMaxValue health;
    private UnitStatsController unitStatsController;

    void Awake()
    {
        if (UnitData is not null)
        {
            Initialize(UnitData);
        }
    }

    public void Initialize(UnitData unitData)
    {
        UnitData = unitData;
        health = new CounterWithMaxValue(unitData.MaxHealth, unitData.MaxHealth);
        IsAlive = true;
        IsInitialized = true;
        unitCollider.enabled = true;

        OnInitialized?.Invoke();
    }

    public void DealDamage(int damage)
    {
        if (!IsAlive)
            return;

        int previousHealth = health.Current;
        health.Remove(damage);
        OnDamageTaken?.Invoke(previousHealth, health.Current);

        if (health.Current <= 0)
        {
            HandleDeath();
        }
    }


    private void HandleDeath()
    {
        IsAlive = false;
        unitCollider.enabled = false;
        StartCoroutine(DestroyAfterDelay(UnitData.DeathAnimDuration));

        OnDeath?.Invoke();
    }

    private void Destroy()
    {
        Destroy(gameObject); // for now, just destroy
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1, 0), unitCollider.radius);
    }
}
