using System.Collections;
using Lean.Pool;
using UnityEngine;

public class View_HitOrMisses : SceneServiceView
{
    [SerializeField] private GameObject hitIndicator;
    [SerializeField] private GameObject missIndicator;
    [SerializeField] private float indicatorDuration = 0.5f;

    private GameObject currentIndicator;
    private Coroutine previousCoroutine;
    public void ShowIndicator(bool isHit)
    {
        if (currentIndicator != null)
        {
            if (previousCoroutine != null)
            {
                StopCoroutine(previousCoroutine);
                previousCoroutine = null;
            }

            LeanPool.Despawn(currentIndicator);
            currentIndicator = null;
        }

        var indicator = isHit ? hitIndicator : missIndicator;
        previousCoroutine = StartCoroutine(SpawnIndicator(indicator));
    }

    private IEnumerator SpawnIndicator(GameObject indicator)
    {
        var spawnedIndicator = LeanPool.Spawn(indicator, transform);
        currentIndicator = spawnedIndicator;

        yield return new WaitForSeconds(indicatorDuration);
        LeanPool.Despawn(spawnedIndicator);
    }
}
