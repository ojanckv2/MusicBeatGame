using System;
using UnityEngine;

public class DekstopInputHandler : MonoBehaviour, IInputHandler
{
    private bool isInitialized = false;

    public event Action OnLeftInput;
    public event Action OnRightInput;
    public event Action OnAttackInput;
    public event Action OnSkillOneInput;
    public event Action OnSkillTwoInput;
    public event Action OnSkillThreeInput;

    public void Initialize()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (isInitialized) return;

        isInitialized = true;
#endif
    }

    private void Update()
    {
        if (!isInitialized) return;

        if (Input.GetKeyDown(KeyCode.A))
            OnLeftInput?.Invoke();
        else if (Input.GetKeyDown(KeyCode.D))
            OnRightInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space))
            OnAttackInput?.Invoke();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            OnSkillOneInput?.Invoke();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            OnSkillTwoInput?.Invoke();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            OnSkillThreeInput?.Invoke();
    }
}
