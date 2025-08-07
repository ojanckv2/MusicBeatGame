using System;
using UnityEngine;

public class MobileInputHandler : MonoBehaviour, IInputHandler
{
    private GUIButtons GUIButtons;

    public event Action OnLeftInput;
    public event Action OnRightInput;
    public event Action OnAttackInput;
    public event Action OnSkillOneInput;
    public event Action OnSkillTwoInput;
    public event Action OnSkillThreeInput;

    public void Initialize()
    {
        SetUpInputs();
    }

    private void SetUpInputs()
    {
        GUIButtons = SceneCoreView.GetSceneServiceView<GUIButtons>();

        if (GUIButtons == null)
        {
            Debug.LogError("GUIButtons not found in the scene.");
            return;
        }

        var buttonMap = GUIButtons.ButtonMap;
        buttonMap["Left"].onPostClick.AddListener(LeftInput);
        buttonMap["Right"].onPostClick.AddListener(RightInput);
        buttonMap["Attack"].onPostClick.AddListener(AttackInput);
        buttonMap["SkillOne"].onPostClick.AddListener(SkillOneInput);
        buttonMap["SkillTwo"].onPostClick.AddListener(SkillTwoInput);
        buttonMap["SkillThree"].onPostClick.AddListener(SkillThreeInput);
    }

    private void LeftInput()
    {
        OnLeftInput?.Invoke();
    }

    private void RightInput()
    {
        OnRightInput?.Invoke();
    }

    private void AttackInput()
    {
        OnAttackInput?.Invoke();
    }

    private void SkillOneInput()
    {
        OnSkillOneInput?.Invoke();
    }

    private void SkillTwoInput()
    {
        OnSkillTwoInput?.Invoke();
    }
    
    private void SkillThreeInput()
    {
        OnSkillThreeInput?.Invoke();
    }
}