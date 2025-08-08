using UnityEngine;

public class EnvironmentAnimation : MonoBehaviour
{
    private EnvironmentAnimator animator;
    public void SetAnimator(EnvironmentAnimator animator)
    {
        this.animator = animator;
    }

    public void HasExitedArea()
    {
        animator.onExitedArea.Invoke(this);
    }
}