using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitFxPrefab;

    private ParticleSystem hitFx;

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

        if (hitFx == null)
        {
            hitFx = Instantiate(hitFxPrefab);
        }
        hitFx.transform.position = target.transform.position + new Vector3(0, 1, 0);
        hitFx.Play();

        CameraShaker.Instance.Shake(Mathf.Lerp(0, 1, attackDamage / 100f));
    }
}