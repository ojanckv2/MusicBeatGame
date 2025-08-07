using UnityEngine;

public class SkillManager : SceneService
{
    [SerializeField] private Skill_AreaDestroy skillAreaDestroy;
    [SerializeField] private Skill_SpawnLaneProtectors skillSpawnLaneProtectors;
    [SerializeField] private Skill_ProtectPlayer skillProtectPlayer;

    public void CastSkillAreaDestroy()
    {
        if (skillAreaDestroy != null)
        {
            skillAreaDestroy.Cast();
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
        }
        else
        {
            Debug.LogWarning("Skill_ProtectPlayer is not active or not assigned.");
        }
    }
}
