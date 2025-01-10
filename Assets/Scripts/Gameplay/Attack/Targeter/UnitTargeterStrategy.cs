using UnityEngine;

/// <summary>
/// Exists only to be serialized in the inspector...
/// </summary>
public abstract class UnitTargeterStrategy : ScriptableObject, IUnitTargeterStrategy
{
    public abstract bool TryToTarget(Transform targeterTrasnform, int range, out Unit unit);
}
