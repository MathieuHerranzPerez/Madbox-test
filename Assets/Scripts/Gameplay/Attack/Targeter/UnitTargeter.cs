using UnityEngine;

public class UnitTargeter : MonoBehaviour
{
    public bool HasTarget => target != null && target.IsAlive;
    public Unit Target => HasTarget ? target : null;

    private IUnitTargeterStrategy unitTargeterStrategy;
    private Unit target;

    public void SetUnitTargeterStrategy(IUnitTargeterStrategy unitTargeterStrategy)
    {
        this.unitTargeterStrategy = unitTargeterStrategy;
    }

    public bool TryToTarget(int range, out Unit unit)
    {
        ResetTarget();

        bool result = unitTargeterStrategy.TryToTarget(transform, range, out target);
        unit = target;
        return result;
    }

    public void ResetTarget()
    {
        target = null;
    }
}
