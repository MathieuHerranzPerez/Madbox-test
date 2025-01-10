using System.Linq;
using UnityEngine;

public class UnitMovementAnimator : MonoBehaviour
{
    private const string animatorTriggerStartRunningName = "startRunning";
    private const string animatorParamSpeedFactorName = "speedFactor";
    private const string animatorParamIsRunningName = "isRunning";

    [SerializeField] private UnitMovementController unitMovementController;
    [SerializeField] private UnitStatsController unitStatsController;
    [SerializeField] private Animator animator;
    [SerializeField] private float distanceRunnedByOneAnimationCycle = 3f;
    [SerializeField] private string animatorMoveClipName = "HeroMove";

    void OnEnable()
    {
        unitMovementController.OnStartRunning += HandleStartRunning;
        unitMovementController.OnRunning += HandleRunning;
        unitMovementController.OnStopRunning += HandleStopRunning;
        unitStatsController.OnSpeedChanged += HandleSpeedChanged;

        ComputeAnimSpeedFactor(unitStatsController.Speed);
    }

    void OnDisable()
    {
        unitMovementController.OnStartRunning -= HandleStartRunning;
        unitMovementController.OnRunning -= HandleRunning;
        unitMovementController.OnStopRunning -= HandleStopRunning;
        unitStatsController.OnSpeedChanged -= HandleSpeedChanged;
    }

    private void HandleStartRunning(Vector2 direction)
    {
        animator.SetTrigger(animatorTriggerStartRunningName);
        animator.SetBool(animatorParamIsRunningName, true);
    }

    private void HandleRunning(Vector2 direction)
    {
        // TODO footsteps FX
    }

    private void HandleStopRunning()
    {
        animator.SetBool(animatorParamIsRunningName, false);
    }

    private void HandleSpeedChanged(float speed)
    {
        ComputeAnimSpeedFactor(speed);
    }

    private void ComputeAnimSpeedFactor(float speed)
    {
        AnimationClip animationClip = animator.runtimeAnimatorController.animationClips.First(clip => clip.name == animatorMoveClipName);
        animator.SetFloat(animatorParamSpeedFactorName, speed * (distanceRunnedByOneAnimationCycle / animationClip.length));
    }
}
