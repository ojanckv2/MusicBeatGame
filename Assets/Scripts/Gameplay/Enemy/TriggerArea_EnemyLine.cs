using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerArea_EnemyLine : MonoBehaviour
{
    private bool[] isLaneProtected = new bool[] { false, false, false }; // Assuming 3 lanes
    [HideInInspector] public UnityEvent<int> onProtectionGone = new(); 

    private PlayerControls player;
    private void Start()
    {
        player = PlayerControls.player;
    }

    private void OnTriggerEnter(Collider other)
    {
        var isEnemy = other.TryGetComponent(out EnemyDummy enemyDummy);
        if (isEnemy) {
            enemyDummy.SetTrueOnTriggerArea();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var isEnemy = other.TryGetComponent(out EnemyDummy enemyDummy);
        if (isEnemy)
        {
            enemyDummy.SetFalseOnTriggerArea();

            var playerLane = player.GetCurrentLane();
            var enemyLane = enemyDummy.GetCurrentLane();
            if (isLaneProtected[enemyLane])
            {
                // Handle if Lane is Protected
                enemyDummy.OnUniquePlayerAttack();
                SetLaneProtection(enemyLane, false);

                onProtectionGone.Invoke(enemyLane);
                return;
            }

            if (playerLane == enemyLane)
            {
                // Handle if Player and Enemy Collides
                if (player.IsProtectionActive) {
                    enemyDummy.OnUniquePlayerAttack();
                    player.ActivatePlayerProtectionBubble(false);
                }
                else
                    enemyDummy.OnPlayerHit();
            }
            else
            {
                // Handle if Player and Enemy Misses
                enemyDummy.OnPlayerMisses();
            }
        }
    }
    
    public void SetLaneProtection(int laneIndex, bool isProtected)
    {
        if (laneIndex < 0 || laneIndex >= isLaneProtected.Length)
            return;

        isLaneProtected[laneIndex] = isProtected;
        Debug.Log($"Lane {laneIndex} protection set to {isProtected}");
    }
}
