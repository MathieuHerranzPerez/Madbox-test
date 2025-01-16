using System;
using UnityEngine;

public class UnitTargeter : MonoBehaviour
{
    public event Action<Unit> OnTargetChanged;

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
        Unit previousTarget = target;

        SetTargetToNull();

        bool result = unitTargeterStrategy.TryToTarget(transform, range, out target);
        unit = target;

        if (previousTarget != target)
            OnTargetChanged?.Invoke(target);

        return result;
    }

    public void ResetTarget()
    {
        Unit previousTarget = target;
        SetTargetToNull();

        if (previousTarget != target)
            OnTargetChanged?.Invoke(target);
    }

    private void SetTargetToNull()
    {
        target = null;
    }
}
