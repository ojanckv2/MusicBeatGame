using System;
using UnityEngine;

public interface IInputHandler
{
    void Initialize();
    event Action OnLeftInput;
    event Action OnRightInput;
    event Action OnAttackInput;
    event Action OnSkillOneInput;
    event Action OnSkillTwoInput;
    event Action OnSkillThreeInput;
}
