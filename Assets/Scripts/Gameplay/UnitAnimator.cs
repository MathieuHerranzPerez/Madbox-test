using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string animatorTriggerDamageTaken = "onDamageTaken";
    private const string animatorTriggerDeath = "onDeath";
    private const string animatorIntHealthAmount = "health";
    private const string animatorDefaultState = "Idle";

    [SerializeField] private Animator animator;
    [SerializeField] private Unit unit;


    void OnEnable()
    {
        unit.OnDamageTaken += HandleDamageTaken;
        unit.OnDeath += HandleDeath;
        unit.OnInitialized += HandleUnitInitialized;

        if (unit.IsInitialized)
            HandleUnitInitialized();
    }

    void OnDisable()
    {
        unit.OnDamageTaken -= HandleDamageTaken;
        unit.OnDeath -= HandleDeath;
        unit.OnInitialized -= HandleUnitInitialized;
    }

    private void HandleUnitInitialized()
    {
        animator.Play(animatorDefaultState);
        animator.SetInteger(animatorIntHealthAmount, unit.CurrentHealth);
    }

    private void HandleDamageTaken(int previousAmount, int currentAmount)
    {
        animator.SetInteger(animatorIntHealthAmount, currentAmount);
        animator.SetTrigger(animatorTriggerDamageTaken);

        // TODO hit effect, damage amount pop
    }

    private void HandleDeath()
    {
        animator.SetTrigger(animatorTriggerDeath);
    }
}
