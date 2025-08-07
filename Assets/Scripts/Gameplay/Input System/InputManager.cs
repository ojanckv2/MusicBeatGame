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

    private bool canInput = true;
    public bool CanInput {
        get => canInput;
        set => canInput = value;
    }

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
        if (!canInput) return;
        onLeftInput?.Invoke();
    }

    private void InvokeRightInput()
    {
        if (!canInput) return;
        onRightInput?.Invoke();
    }

    private void InvokeAttackInput()
    {
        if (!canInput) return;
        onAttackInput?.Invoke();
    }

    private void InvokeSkillOneInput()
    {
        if (!canInput) return;
        onSkillOneInput?.Invoke();
    }

    private void InvokeSkillTwoInput()
    {
        if (!canInput) return;
        onSkillTwoInput?.Invoke();
    }

    private void InvokeSkillThreeInput()
    {
        if (!canInput) return;
        onSkillThreeInput?.Invoke();
    }
}
