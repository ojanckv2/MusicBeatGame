using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Skill_AreaDestroy : MonoBehaviour, ISkillHandler
{
    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;
    [SerializeField] private float cooldownTime;
    public string SkillName => skillName;
    public string SkillDescription => skillDescription;
    public float CooldownTime => cooldownTime;

    [SerializeField] private ParticleSystem onSkillCastVFX;
    [SerializeField] private float VFXPosY = 0.0725f;

    private bool isActive = true;
    public void Cast()
    {
        if (!isActive) return;
        
        Debug.Log($"Casting skill: {skillName}");

        foreach (var enemy in enemiesInRange)
        {
            if (enemy == null) continue;

            if (onSkillCastVFX != null)
            {
                StartCoroutine(SpawnOnSkillCastVFX(enemy.transform));
            }

            enemy.OnUniquePlayerAttack(); // Assuming TakeDamage is a method in EnemyDummy
        }

        enemiesInRange.Clear();
        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        isActive = false;

        yield return new WaitForSeconds(cooldownTime);

        isActive = true;
    }

    private IEnumerator SpawnOnSkillCastVFX(Transform enemyTransform)
    {
        var vfx = LeanPool.Spawn(onSkillCastVFX, enemyTransform.position, Quaternion.identity);
        vfx.transform.position = new Vector3(vfx.transform.position.x, VFXPosY, vfx.transform.position.z);
        vfx.Play();

        yield return new WaitForSeconds(3f);

        LeanPool.Despawn(vfx.gameObject);
    }

    private List<EnemyDummy> enemiesInRange = new List<EnemyDummy>();
    private void OnTriggerEnter(Collider other)
    {
        var isEnemy = other.gameObject.TryGetComponent(out EnemyDummy enemy);
        if (isEnemy == false)
            return;

        enemiesInRange.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        var isEnemy = other.gameObject.TryGetComponent(out EnemyDummy enemy);
        if (isEnemy == false)
            return;

        enemiesInRange.Remove(enemy);
    }
}
