using System.Collections.Generic;
using UnityEngine;

public class GUIButtons : SceneServiceView
{
    [SerializeField] private ButtonImproved leftButton;
    [SerializeField] private ButtonImproved rightButton;
    [SerializeField] private ButtonImproved attackButton;
    [SerializeField] private ButtonImproved skillOneButton;
    [SerializeField] private ButtonImproved skillTwoButton;
    [SerializeField] private ButtonImproved skillThreeButton;

    private Dictionary<string, ButtonImproved> buttonMap = new();
    public Dictionary<string, ButtonImproved> ButtonMap => buttonMap;

    protected override void OnActivate()
    {
        buttonMap.Add("Left", leftButton);
        buttonMap.Add("Right", rightButton);
        buttonMap.Add("Attack", attackButton);
        buttonMap.Add("SkillOne", skillOneButton);
        buttonMap.Add("SkillTwo", skillTwoButton);
        buttonMap.Add("SkillThree", skillThreeButton);
    }
}