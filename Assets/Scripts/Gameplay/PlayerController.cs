using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovementInputsGetter playerInputs;
    [SerializeField] private UnitMovementController unitMovementController;
    [SerializeField] private UnitTargeter unitTargeter;
    [SerializeField] private UnitWeaponedAttackController unitWeaponedAttackController;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private UnitStatsController unitStatsController;

    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;

        unitStatsController.Initialize(playerData);
        unitTargeter.SetUnitTargeterStrategy(playerData.UnitTargeterStrategy);
    }

    void Update()
    {
        Vector3 inputMovement = new Vector3(playerInputs.Horizontal, 0f, playerInputs.Verical);

        Vector3 desiredMovement = new Vector3(0f, 0f, 0f);
        if (playerInputs.IsInputDetected)
        {
            unitWeaponedAttackController.SetCanAttack(false);

            // Compute the movement based on camera rotation
            desiredMovement = Quaternion.Euler(0f, mainCam.transform.rotation.eulerAngles.y, 0f) * inputMovement;

            unitTargeter.ResetTarget();
        }
        else
        {
            if (!unitTargeter.HasTarget)
            {
                unitTargeter.TryToTarget(playerData.RangeAutoTarget, out Unit _);
            }

            if (unitTargeter.HasTarget)
            {
                Unit target = unitTargeter.Target;

                unitWeaponedAttackController.SetTarget(target);

                Vector3 targetDirection = target.transform.position - transform.position;
                unitMovementController.LookAt(targetDirection, Time.deltaTime);

                unitWeaponedAttackController.SetCanAttack(IsTargetInRange(targetDirection));
            }
            else
            {
                unitWeaponedAttackController.SetCanAttack(false);
            }
        }

        unitMovementController.Move(desiredMovement, Time.deltaTime);
    }

    private bool IsTargetInRange(Vector3 targetDirection)
    {
        return unitWeaponedAttackController.Range * unitWeaponedAttackController.Range >= targetDirection.sqrMagnitude;
    }
}
