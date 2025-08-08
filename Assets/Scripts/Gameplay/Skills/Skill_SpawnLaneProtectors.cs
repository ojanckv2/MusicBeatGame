using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Skill_SpawnLaneProtectors : MonoBehaviour, ISkillHandler
{
    [SerializeField] private string skillName = "Spawn Lane Protectors";
    [SerializeField] private string skillDescription = "Spawns lane protectors to shield the player from incoming attacks.";
    [SerializeField] private float cooldownTime = 10f;
    public string SkillName => skillName;
    public string SkillDescription => skillDescription;
    public float CooldownTime => cooldownTime;

    [SerializeField] private GameObject laneProtectorPrefab;
    [SerializeField] private TriggerArea_EnemyLine triggerAreaEnemyLine;
    [SerializeField] private Vector3[] lanePositions = new Vector3[3];
    private bool isActive = true;

    private void Awake()
    {
        triggerAreaEnemyLine.onProtectionGone.AddListener(DespawnLaneProtector);
    }

    public void Cast()
    {
        if (!isActive) return;

        // Logic to spawn lane protectors
        Debug.Log($"{SkillName} casted! Spawning lane protectors...");
        // Implement the actual spawning logic here
        SpawnLaneProtectors();

        StartCoroutine(CooldownTimer());
    }

    private readonly Dictionary<int, GameObject> laneProtectors = new();
    private void SpawnLaneProtectors()
    {
        ClearLaneProtectors(); // Clear existing lane protectors

        for (int i = 0; i < 3; i++) // Assuming 3 lanes
        {
            var protector = LeanPool.Spawn(laneProtectorPrefab, lanePositions[i], Quaternion.identity);
            laneProtectors.Add(i, protector);

            triggerAreaEnemyLine.SetLaneProtection(i, true); // Set lane protection
        }
    }

    private void ClearLaneProtectors()
    {
        foreach (var keyValuePair in laneProtectors) {
            LeanPool.Despawn(keyValuePair.Value);
        }
        laneProtectors.Clear();
    }

    private void DespawnLaneProtector(int index)
    {
        var hasProtector = laneProtectors.ContainsKey(index);
        if (hasProtector == false) return;

        var protector = laneProtectors[index];
        if (protector != null)
        {
            LeanPool.Despawn(protector);
            laneProtectors.Remove(index);
        }
    }

    private IEnumerator CooldownTimer()
    {
        isActive = false;

        yield return new WaitForSeconds(cooldownTime);

        isActive = true;
    }
}