using UnityEngine;

public class EnemyDummy_SingleMovement : EnemyDummy
{
    [Header("Beat Settings")]
    public Vector3 direction = Vector3.back; // Movement direction
    private float speed; // Final speed (units/second)

    protected override void OnActivate()
    {
        base.OnActivate();

        float secondsPerBeat = 60f / levelManager.BPM;
        speed = levelManager.DistancePerBeat / secondsPerBeat;
    }

    private void Update()
    {
        if (!isActive) return;

        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
