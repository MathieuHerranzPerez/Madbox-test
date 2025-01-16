using Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance { get; private set; }

    [SerializeField] private CinemachineImpulseSource impulseSource;

    void Awake()
    {
        Instance = this;
    }

    public void Shake(float intensity = 1f)
    {
        impulseSource.GenerateImpulse(intensity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
    }
}
