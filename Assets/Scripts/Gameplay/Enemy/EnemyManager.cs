using UnityEngine;

public class EnemyManager : SceneService
{
    private LevelManager levelManager;
    private InputManager inputManager;
    private View_HitOrMisses viewHitOrMisses;

    [SerializeField] private EnemyDummy[] enemies;
    public int GetEnemyCount()
    {
        if (enemies == null || (enemies == null && enemies.Length == 0))
        {
            Debug.LogWarning("No enemies assigned to EnemyManager.");
            return 0;
        }

        return enemies.Length;
    }
    
    protected override void OnActivate()
    {
        levelManager = SceneCore.GetService<LevelManager>();
        inputManager = SceneCore.GetService<InputManager>();
        viewHitOrMisses = SceneCoreView.GetSceneServiceView<View_HitOrMisses>();

        if (enemies == null || (enemies == null && enemies.Length == 0))
        {
            Debug.LogWarning("No enemies assigned to EnemyManager.");
            return;
        }

        // Initialize enemies or any other setup needed
        foreach (var enemy in enemies)
        {
            enemy.SetLevelManager(levelManager);
            enemy.SetInputManager(inputManager);
            enemy.SetViewHitOrMisses(viewHitOrMisses);
            enemy.Activate();
        }
    }
}
