using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitWeaponedAttackAnimator : MonoBehaviour
{
    private const string animatorAttackClipName = "HeroAttack";
    private const string animatorTriggerAutoAttackName = "attack";
    private const string animatorAttackSpeedFactorName = "attackSpeedFactor";

    [SerializeField] private UnitStatsController unitStatsController;

    [Space(20)]
    [SerializeField] private UnitWeaponedAttackController unitWeaponedAttackController;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponSlot;

    private string attackAnimationClipName => weaponData != null ? weaponData.AnimationClip.name : animatorAttackClipName;
    private WeaponData weaponData;
    private GameObject weaponSkinGO;

    private AnimatorOverrideController overrideController;
    private AnimationClipOverrides clipOverrides;

    void OnEnable()
    {
        unitWeaponedAttackController.OnAttackLaunched += LaunchAttack;
        unitWeaponedAttackController.OnWeaponChanged += HandleAttackChanged;

        unitStatsController.OnRateChanged += HandleRateChanged;
    }

    void OnDisable()
    {
        unitWeaponedAttackController.OnAttackLaunched -= LaunchAttack;
        unitWeaponedAttackController.OnWeaponChanged -= HandleAttackChanged;

        unitStatsController.OnRateChanged -= HandleRateChanged;
    }

    void Start()
    {
        ComputeAnimationSpeed();
    }

    // Called from Animator Event
    public void TriggerHit()
    {
        unitWeaponedAttackController.TriggerHit();
    }

    /// <summary>
    /// /!\ This function changes the animator's RuntimeAnimatorController to override the attack animation
    /// </summary>
    /// <param name="weapon"></param>
    private void HandleAttackChanged(WeaponData weaponData)
    {
        string lastAnimClipName = attackAnimationClipName;

        this.weaponData = weaponData;
        HandleNewWeaponSkin();
        OverrideAttackAnimation(lastAnimClipName);

        unitStatsController.SetDefaultAttackDamage(weaponData.AttackDamage);
        unitStatsController.SetDefaultRange(weaponData.Range);
        unitStatsController.SetDefaultAttackRate(weaponData.AttackRate);
        unitStatsController.AddBonusTermSpeed(weaponData.BonusMovementSpeed);
        ComputeAnimationSpeed();
    }


    private void LaunchAttack()
    {
        animator.SetTrigger(animatorTriggerAutoAttackName);
    }

    private void HandleNewWeaponSkin()
    {
        if (weaponSkinGO != null)
            Destroy(weaponSkinGO);

        weaponSkinGO = Instantiate(weaponData.SkinPrefab, weaponSlot);
    }

    private void HandleRateChanged(float rate)
    {
        ComputeAnimationSpeed();
    }

    private void ComputeAnimationSpeed()
    {
        string attackClipName = attackAnimationClipName;
        AnimationClip animationClip = animator.runtimeAnimatorController.animationClips.First(clip => clip.name == attackClipName);
        animator.SetFloat(animatorAttackSpeedFactorName, animationClip.length / unitStatsController.AttackRate);
    }

    private void OverrideAttackAnimation(string animationAttackName)
    {
        overrideController ??= new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        clipOverrides = new AnimationClipOverrides(overrideController.overridesCount);
        overrideController.GetOverrides(clipOverrides);

        clipOverrides[animationAttackName] = weaponData.AnimationClip;
        overrideController.ApplyOverrides(clipOverrides);
    }
}


public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) { }

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}