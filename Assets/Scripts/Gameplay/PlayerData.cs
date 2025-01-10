using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : UnitData
{
    [Header("Targeting")]
    [Range(1, 20)]
    public int RangeAutoTarget = 10;
}
