using UnityEngine;

[CreateAssetMenu(fileName = "UnitFactoryData", menuName = "Gameplay/UnitFactoryData")]
public class UnitFactoryData : ScriptableObject
{
    [SerializeField] private UnitData unitData;

    /// <summary>
    /// Depends on what we need, we can do pooling here, or maybe changing the prefab or the strength of the Unit as the game goes
    /// </summary>
    /// <returns>Unit</returns>
    public Unit CreateUnit(Transform container)
    {
        Unit unit = Instantiate(unitData.UnitPrefab, container);
        unit.Initialize(unitData);
        return unit;
    }
}
