using UnityEngine;

public abstract class PlayerMovementInputsGetter : MonoBehaviour
{
    public float Horizontal { get { return input.x; } }
    public float Verical { get { return input.y; } }
    public bool IsInputDetected { get; protected set; }

    protected Vector2 input = Vector2.zero;
}
