using System.Collections;
using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDummy : MonoBehaviour
{
    protected LevelManager levelManager;
    protected View_HitOrMisses viewHitOrMisses;
    protected InputManager inputManager;

    protected bool isActive = false;
    public bool IsActive => isActive;

    [SerializeField] private ParticleSystem explodeVFX;
    [SerializeField] private int currentLane = 1;
    public int GetCurrentLane() => currentLane;

    private bool isOnTriggerArea = false;

    public void SetLevelManager(LevelManager level)
    {
        levelManager = level;
    }

    public void SetInputManager(InputManager input)
    {
        inputManager = input;
    }

    public void SetViewHitOrMisses(View_HitOrMisses view)
    {
        viewHitOrMisses = view;
    }

    public void Activate()
    {
        if (isActive) return;

        OnActivate();
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive) return;

        OnDeactivate();
        isActive = false;
    }

    protected virtual void OnActivate()
    {
        inputManager.onAttackInput.AddListener(OnPlayerAttack);
    }

    protected virtual void OnDeactivate()
    {
        inputManager.onAttackInput.RemoveListener(OnPlayerAttack);
        inputManager = null;
    }

    private void Destroy()
    {
        Deactivate();

        StartCoroutine(SpawnExplodeVFX());
        LeanPool.Despawn(gameObject);
    }

    private IEnumerator SpawnExplodeVFX()
    {
        var explode = LeanPool.Spawn(explodeVFX, transform.position, Quaternion.identity);
        explode.Play();

        yield return new WaitForSeconds(1f);

        LeanPool.Despawn(explode);
    }

    public void OnPlayerHit()
    {
        Debug.Log("Player Hit!");

        viewHitOrMisses.ShowIndicator(false);
        levelManager.PlayerHit();
        Destroy();
    }

    private void OnPlayerAttack()
    {
        if (!isOnTriggerArea) return;

        // Handle enemy being attacked logic here
        Debug.Log("Enemy Dummy attacked!");

        viewHitOrMisses.ShowIndicator(true);
        levelManager.EnemyKilled();
        Destroy();
    }

    public void OnUniquePlayerAttack()
    {
        // Handle unique player attack logic here
        Debug.Log("Unique Player Attack on Enemy Dummy!");

        viewHitOrMisses.ShowIndicator(true);
        levelManager.EnemyKilled();
        Destroy();
    }

    public void OnPlayerMisses()
    {
        Debug.Log("Player Misses!");

        StartCoroutine(despawnAfterDelay());
        viewHitOrMisses.ShowIndicator(false);

        IEnumerator despawnAfterDelay()
        {
            yield return new WaitForSeconds(0.2f);

            Deactivate();
            LeanPool.Despawn(gameObject);
        }
    }

    public void SetTrueOnTriggerArea()
    {
        isOnTriggerArea = true;
    }

    public void SetFalseOnTriggerArea()
    {
        isOnTriggerArea = false;
    }
}
