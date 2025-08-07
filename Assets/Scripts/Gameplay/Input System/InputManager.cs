using UnityEngine;
using UnityEngine.Events;

public class InputManager : SceneService
{
    [SerializeField] private MobileInputHandler mobileInputHandler;
    [SerializeField] private DekstopInputHandler desktopInputHandler;

    [HideInInspector] public UnityEvent onLeftInput = new();
    [HideInInspector] public UnityEvent onRightInput = new();
    [HideInInspector] public UnityEvent onAttackInput = new();
    [HideInInspector] public UnityEvent onSkillOneInput = new();
    [HideInInspector] public UnityEvent onSkillTwoInput = new();
    [HideInInspector] public UnityEvent onSkillThreeInput = new();

    protected override void OnActivate()
    {
        mobileInputHandler.Initialize();
        desktopInputHandler.Initialize();

        RegisterCallback(mobileInputHandler, desktopInputHandler);
    }

    private void RegisterCallback(params IInputHandler[] inputHandlers)
    {
        foreach (var inputHandler in inputHandlers) {
            inputHandler.OnLeftInput += InvokeLeftInput;
            inputHandler.OnRightInput += InvokeRightInput;
            inputHandler.OnAttackInput += InvokeAttackInput;
            inputHandler.OnSkillOneInput += InvokeSkillOneInput;
            inputHandler.OnSkillTwoInput += InvokeSkillTwoInput;
            inputHandler.OnSkillThreeInput += InvokeSkillThreeInput;
        }
    }

    private void InvokeLeftInput()
    {
        onLeftInput?.Invoke();
    }

    private void InvokeRightInput()
    {
        onRightInput?.Invoke();
    }

    private void InvokeAttackInput()
    {
        onAttackInput?.Invoke();
    }

    private void InvokeSkillOneInput()
    {
        onSkillOneInput?.Invoke();
    }

    private void InvokeSkillTwoInput()
    {
        onSkillTwoInput?.Invoke();
    }

    private void InvokeSkillThreeInput()
    {
        onSkillThreeInput?.Invoke();
    }
}
