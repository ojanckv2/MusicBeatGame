using UnityEngine;

public class EnemyDummy_SingleMovement : EnemyDummy
{
    private bool isPaused = false;

    [Header("Beat Settings")]
    public Vector3 direction = Vector3.back; // Movement direction
    private float speed; // Final speed (units/second)

    protected override void OnActivate()
    {
        base.OnActivate();

        float secondsPerBeat = 60f / levelManager.BPM;
        speed = levelManager.DistancePerBeat / secondsPerBeat;

        levelManager.onLevelEnd.AddListener(OnLevelPaused);
        levelManager.onLevelPaused.AddListener(OnLevelPaused);
        levelManager.onLevelResumed.AddListener(OnLevelResumed);
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();

        levelManager.onLevelEnd.RemoveListener(OnLevelPaused);
        levelManager.onLevelPaused.RemoveListener(OnLevelPaused);
        levelManager.onLevelResumed.RemoveListener(OnLevelResumed);
    }

    private void Update()
    {
        if (!isActive) return;
        if (isPaused) return;

        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnLevelPaused()
    {
        isPaused = true;
    }

    private void OnLevelResumed()
    {
        isPaused = false;
    }
}
