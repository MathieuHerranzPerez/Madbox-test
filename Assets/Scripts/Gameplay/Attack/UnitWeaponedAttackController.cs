using System;
using UnityEngine;

public class UnitWeaponedAttackController : UnitAttackController
{
    public event Action<WeaponData> OnWeaponChanged;

    [SerializeField] private Transform weaponSlot;

    private WeaponData weaponData;
    private Weapon weapon;

    // Called from UnityEvent
    public void EquipWeapon(WeaponData weaponData)
    {
        if (this.weaponData == weaponData)
            return;

        Debug.Log($"UnitWeaponedAttackController - EquipWeapon : {weaponData.name}");

        weapon = Instantiate(weaponData.WeaponPrefab, transform);

        OnWeaponChanged?.Invoke(weaponData);
    }

    public override void TriggerHit()
    {
        if (weapon != null)
        {
            weapon.TriggerHit(target, unitStatsController.AttackDamage, unitStatsController.Range);
        }
        else
        {
            base.TriggerHit();
        }
    }

    protected override void LaunchAttack()
    {
        base.LaunchAttack();

        weapon?.LaunchAttack();
    }

    protected override void CancelAttack()
    {
        base.CancelAttack();

        weapon?.CancelAttack();
    }
}