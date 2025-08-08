using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerArea_EnvironmentRestart : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        var hasEnvAnim = other.TryGetComponent<EnvironmentAnimation>(out var envAnim);
        if (hasEnvAnim)
        {
            envAnim.HasExitedArea();
        }
    }
}
