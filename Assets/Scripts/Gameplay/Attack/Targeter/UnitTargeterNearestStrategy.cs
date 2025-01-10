using UnityEngine;

[CreateAssetMenu(fileName = "UnitTargeterNearestStrategy", menuName = "Gameplay/Targeter/UnitTargeterNearestStrategy")]
public class UnitTargeterNearestStrategy : UnitTargeterStrategy
{
    [SerializeField] private LayerMask unitMaskToTarget;

    public override bool TryToTarget(Transform targeterTransform, int range, out Unit unit)
    {
        unit = null;

        Collider[] colliders = Physics.OverlapSphere(targeterTransform.position, range, unitMaskToTarget);
        float minDistanceSqr = Mathf.Infinity;
        Collider nearestCollider = null;
        foreach (Collider collider in colliders)
        {
            float distanceSqr = (collider.transform.position - targeterTransform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                // TODO can raycast to check walls

                nearestCollider = collider;
                minDistanceSqr = distanceSqr;
            }
        }

        if (nearestCollider != null)
        {
            unit = nearestCollider.GetComponent<Unit>();
            return true;
        }

        return false;
    }
}
