using UnityEngine;

public interface IUnitTargeterStrategy
{
    public bool TryToTarget(Transform targeterTrasnform, int range, out Unit unit);
}
