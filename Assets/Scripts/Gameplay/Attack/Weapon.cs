using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void LaunchAttack()
    {
        // TODO Fx
    }

    public void CancelAttack()
    {
        // TODO stop Fx
    }

    public void TriggerHit(Unit target, int attackDamage, float range)
    {
        // TODO basic weapon for now, but we can make more complex weapons with some AOE, projectiles...
        // I will check later if enough time

        if (target == null || !target.IsAlive)
            return;

        // Check range
        if ((transform.position - target.transform.position).sqrMagnitude > (range + target.Radius) * (range + target.Radius))
            return;

        // TODO can raycast to check visibility (walls...)

        target.DealDamage(attackDamage);
    }
}