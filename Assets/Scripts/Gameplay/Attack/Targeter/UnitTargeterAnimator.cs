using UnityEngine;

public class UnitTargeterAnimator : MonoBehaviour
{
    [SerializeField] private UnitTargeter unitTargeter;
    [SerializeField] private GameObject targetIndicator;

    void OnEnable()
    {
        unitTargeter.OnTargetChanged += HandleTargetChanged;
    }

    void OnDisable()
    {
        unitTargeter.OnTargetChanged -= HandleTargetChanged;
    }

    private void HandleTargetChanged(Unit target)
    {
        if (target != null)
        {
            targetIndicator.SetActive(true);
            targetIndicator.transform.SetParent(target.transform, false);
            targetIndicator.transform.localPosition = new Vector3(0, 0.1f, 0);
        }
        else
        {
            targetIndicator.SetActive(false);
            targetIndicator.transform.SetParent(transform);
        }
    }
}
