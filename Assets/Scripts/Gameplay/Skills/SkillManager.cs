using UnityEngine;

public class SkillManager : SceneService
{
    private GUIButtons GUIButtons;
    [SerializeField] private Skill_AreaDestroy skillAreaDestroy;
    [SerializeField] private Skill_SpawnLaneProtectors skillSpawnLaneProtectors;
    [SerializeField] private Skill_ProtectPlayer skillProtectPlayer;

    protected override void OnActivate()
    {
        GUIButtons = SceneCoreView.GetSceneServiceView<GUIButtons>();
    }

    public void CastSkillAreaDestroy()
    {
        if (skillAreaDestroy != null)
        {
            skillAreaDestroy.Cast();
            var skillButton = GUIButtons.GetSkillButton(1) as ButtonSkill;
            skillButton.ShowCooldown(skillAreaDestroy.CooldownTime);
        }
        else
        {
            Debug.LogWarning("Skill_AreaDestroy is not active or not assigned.");
        }
    }

    public void CastSkillSpawnLaneProtectors()
    {
        if (skillSpawnLaneProtectors != null)
        {
            skillSpawnLaneProtectors.Cast();
            var skillButton = GUIButtons.GetSkillButton(2) as ButtonSkill;
            skillButton.ShowCooldown(skillSpawnLaneProtectors.CooldownTime);
        }
        else
        {
            Debug.LogWarning("Skill_SpawnLaneProtectors is not active or not assigned.");
        }
    }

    public void CastSkillProtectPlayer()
    {
        if (skillProtectPlayer != null)
        {
            skillProtectPlayer.Cast();
            var skillButton = GUIButtons.GetSkillButton(3) as ButtonSkill;
            skillButton.ShowCooldown(skillProtectPlayer.CooldownTime);
        }
        else
        {
            Debug.LogWarning("Skill_ProtectPlayer is not active or not assigned.");
        }
    }
}
