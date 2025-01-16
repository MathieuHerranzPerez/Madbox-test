using System;
using System.Collections;
using UnityEngine;

public class UnitMovementController : MonoBehaviour
{
    #region events
    /// <summary>
    /// Vector2 = direction
    /// </summary>
    public event Action<Vector2> OnStartRunning;
    /// <summary>
    /// Vector2 = direction
    /// </summary>
    public event Action<Vector2> OnRunning;
    public event Action OnStopRunning;
    #endregion

    [SerializeField] private CharacterController characterController;
    [SerializeField] private float minimumSqrMagnitudeToMove = 0.01f;
    [SerializeField] private UnitStatsController unitStatsController;

    private Vector3 desiredMovement;
    private bool isRunning = false;
    private bool isDashing = false;

    public void Move(Vector3 inputMovement, float deltaTime)
    {
        desiredMovement = inputMovement;

        if (!ShouldPerformMovement())
        {
            TryStopMovement();
            return;
        }

        desiredMovement = inputMovement.normalized; // We want full speed all the time => normalize the desiredMovement

        TryStartMovement();
        PerformMovement(deltaTime);
        FaceMovement(deltaTime);
    }

    public void LookAt(Vector3 direction, float deltaTime)
    {
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, unitStatsController.AngularSpeed * deltaTime);
    }

    public void Dash(Vector3 targetPosition, float range)
    {
        if (isDashing)
            return;

        StartCoroutine(DoDash(targetPosition, 10, range));
    }

    private IEnumerator DoDash(Vector3 targetPosition, float speed, float allowedRange)
    {
        isDashing = true;

        while ((targetPosition - transform.position).sqrMagnitude > allowedRange * allowedRange)
        {
            characterController.Move((targetPosition - transform.position).normalized * speed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }




    private void PerformMovement(float deltaTime)
    {
        isRunning = true;
        characterController.Move(desiredMovement * unitStatsController.Speed * deltaTime);

        OnRunning?.Invoke(desiredMovement);
    }

    private void FaceMovement(float deltaTime)
    {
        LookAt(desiredMovement, deltaTime);
    }

    private bool ShouldPerformMovement()
    {
        return desiredMovement.sqrMagnitude >= minimumSqrMagnitudeToMove;
    }

    private void TryStartMovement()
    {
        if (isRunning)
            return;

        isRunning = true;

        OnStartRunning?.Invoke(desiredMovement);
    }

    private void TryStopMovement()
    {
        if (!isRunning) 
            return;

        isRunning = false;

        OnStopRunning?.Invoke();
    }
}
