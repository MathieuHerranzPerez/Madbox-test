using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Gameplay/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Weapon WeaponPrefab;

    [Space(20)]
    public Sprite Icon;
    public GameObject SkinPrefab;
    public AnimationClip AnimationClip;

    [Space(20)]
    public float AttackRate = 1f;
    public int AttackDamage = 20;
    public float Range = 5;

    public float BonusMovementSpeed = 0;
}
