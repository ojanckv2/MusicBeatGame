using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : SceneService
{
    private AudioManager audioManager;
    private EnemyManager enemyManager;

    [SerializeField] private string musicCode = "BP_HowYouLikeThat";
    [SerializeField] private float bpm = 120f; // Beats per minute
    public float BPM => bpm;

    [SerializeField] private float distancePerBeat = 5f; // Distance to move per beat
    public float DistancePerBeat => distancePerBeat;

    [SerializeField] private int playerHealth = 3;
    [SerializeField] private int levelTimer = 20;
    private int enemiesKilled = 0;
    private int numberOfEnemies;

    public UnityEvent onLevelEnd = new();
    public UnityEvent onLevelPaused = new();
    public UnityEvent onLevelResumed = new();

    private void PauseLevel()
    {
        audioManager.PauseMusic();
        audioManager.PauseSFX();
        onLevelPaused.Invoke();
    }

    private void ResumeLevel()
    {
        audioManager.ResumeMusic();
        audioManager.ResumeSFX();
        onLevelResumed.Invoke();
    }

    private Coroutine levelTimerCoroutine;
    protected override void OnActivate()
    {
        audioManager = SceneCore.GetService<AudioManager>();
        enemyManager = SceneCore.GetService<EnemyManager>();

        // Initialize level settings, such as number of enemies
        if (enemyManager == null)
            Debug.LogError("EnemyManager is not assigned or not found in the scene.");
        else {
            numberOfEnemies = enemyManager.GetEnemyCount();
        }

        levelTimerCoroutine = StartCoroutine(StartLevelTimer());
        audioManager.PlayMusic(musicCode);
    }

    private IEnumerator StartLevelTimer()
    {
        yield return new WaitForSeconds(levelTimer);
        EndLevel(true); // End the level if time runs out
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    public void PlayerHit()
    {
        playerHealth--;
        if (playerHealth <= 0)
        {
            Debug.Log("Player has been defeated!");
            StopCoroutine(levelTimerCoroutine);
            EndLevel(false);
        }
    }

    private void EndLevel(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("Level Completed!");
            // Handle level completion logic
        }
        else
        {
            Debug.Log("Level Failed!");
            // Handle level failure logic
        }

        var totalScore = CalculateScore();
        Debug.Log($"Total Score: {totalScore}");

        var percentage = GetPercentage();
        Debug.Log($"Completion Percentage: {percentage}%");
        
        var stars = percentage switch {
            >= 100 => 3,
            >= 70 => 2,
            >= 50 => 1,
            _ => 0
        };
        Debug.Log($"Stars Earned: {stars}");
        
        onLevelEnd.Invoke();
        audioManager.StopMusic();
        PlayerControls.player.SetIdle();
    }

    private int CalculateScore()
    {
        // Implement score calculation logic based on enemies killed, time left, etc.
        return enemiesKilled * 100; // Example scoring system
    }

    private float GetPercentage()
    {
        if (numberOfEnemies <= 0) return 0f;
        return (float)enemiesKilled / numberOfEnemies * 100f;
    }
}
