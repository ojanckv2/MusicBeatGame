using System.Collections;
using UnityEngine;

public class Skill_ProtectPlayer : MonoBehaviour, ISkillHandler
{
    [SerializeField] private string skillName = "Protect Player";
    [SerializeField] private string skillDescription = "Creates a shield around the player.";
    [SerializeField] private float cooldownTime = 10f;

    public string SkillName => skillName;
    public string SkillDescription => skillDescription;
    public float CooldownTime => cooldownTime;

    private PlayerControls player;
    private void Start()
    {
        player = PlayerControls.player;
    }

    private bool isActive = true;
    public void Cast()
    {
        if (!isActive) return;
        if (player.IsProtectionActive) return;

        // Implement the skill logic here
        Debug.Log($"{SkillName} casted!");
        player.ActivatePlayerProtectionBubble(true);

        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        isActive = false;

        yield return new WaitForSeconds(cooldownTime);

        isActive = true;
    }
}
