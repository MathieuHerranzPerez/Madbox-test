using UnityEngine;

public class UnitHitDamageDisplayer : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private DamageHitAmountDisplayer damageHitAmountDisplayer;

    void OnEnable()
    {
        unit.OnDamageTaken += HandleDamageTaken;
    }

    void OnDisable()
    {
        unit.OnDamageTaken -= HandleDamageTaken;
    }

    private void HandleDamageTaken(int oldAmount, int amount)
    {
        // TODO pooling...

        damageHitAmountDisplayer.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), 1.5f, 0);
        damageHitAmountDisplayer.SetAmount(oldAmount - amount);
        damageHitAmountDisplayer.Play();
    }
}
