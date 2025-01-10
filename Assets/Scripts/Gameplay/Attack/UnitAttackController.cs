using System;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    public event Action OnAttackLaunched;

    [SerializeField] protected UnitStatsController unitStatsController;

    protected Unit target;
    protected float timeSinceLastAttack = 4096f;
    private bool canAttack;

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!canAttack)
            return;

        if (timeSinceLastAttack >= unitStatsController.AttackRate)
        {
            LaunchAttack();
            timeSinceLastAttack = 0;
        }
    }

    public void SetCanAttack(bool canAttack)
    {
        if (this.canAttack == canAttack)
            return;

        this.canAttack = canAttack;

        if (!canAttack)
        {
            CancelAttack();
        }
    }

    public void SetTarget(Unit unit)
    {
        target = unit;
    }

    /// <summary>
    /// This function must be called to apply the damage
    /// </summary>
    public virtual void TriggerHit()
    {
        TriggerAutoAttack();
    }

    protected virtual void LaunchAttack()
    {
        OnAttackLaunched?.Invoke();
    }

    protected virtual void CancelAttack()
    {

    }

    protected void TriggerAutoAttack()
    {
        if (target == null || !target.IsAlive)
            return;

        // Check range
        if ((transform.position - target.transform.position).sqrMagnitude > (unitStatsController.Range + target.Radius) * (unitStatsController.Range + target.Radius))
            return;

        // TODO can raycast to check visibility (walls...)

        target.DealDamage(unitStatsController.AttackDamage);
    }




    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position + new Vector3(0, 1, 0), 
            transform.position + transform.forward * unitStatsController.Range + new Vector3(0, 1, 0));
    }
}
