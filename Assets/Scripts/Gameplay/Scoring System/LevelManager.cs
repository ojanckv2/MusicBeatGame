using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : SceneService
{
    private InputManager inputManager;
    private AudioManager audioManager;
    private EnemyManager enemyManager;

    private View_PauseButton view_PauseButton;
    private View_GameMenu view_GameMenu;
    private View_Heart view_Heart;
    private View_GameResult view_GameResult;

    [SerializeField] private string musicCode = "BP_HowYouLikeThat";
    [SerializeField] private float bpm = 120f; // Beats per minute
    public float BPM => bpm;

    [SerializeField] private float distancePerBeat = 5f; // Distance to move per beat
    public float DistancePerBeat => distancePerBeat;

    [SerializeField] private int playerHealth = 3;
    [SerializeField] private int levelTimer = 20;
    private int enemiesKilled = 0;
    private int numberOfEnemies;

    public UnityEvent onLevelStarted = new();
    public UnityEvent onLevelEnd = new();
    public UnityEvent onLevelPaused = new();
    public UnityEvent onLevelResumed = new();

    private void PauseLevel()
    {
        audioManager.PauseMusic();
        audioManager.PauseSFX();
        onLevelPaused.Invoke();

        view_GameMenu.Show();
        inputManager.CanInput = false;
    }

    private void ResumeLevel()
    {
        audioManager.ResumeMusic();
        audioManager.ResumeSFX();
        onLevelResumed.Invoke();
        
        inputManager.CanInput = true;
    }

    private Coroutine levelTimerCoroutine;
    protected override void OnActivate()
    {
        inputManager = SceneCore.GetService<InputManager>();
        audioManager = SceneCore.GetService<AudioManager>();
        enemyManager = SceneCore.GetService<EnemyManager>();

        view_GameResult = SceneCoreView.GetSceneServiceView<View_GameResult>();
        view_GameResult.onBtnQuitClick.AddListener(QuitGame);
        view_GameResult.onBtnRestartClick.AddListener(RestartLevel);

        view_PauseButton = SceneCoreView.GetSceneServiceView<View_PauseButton>();
        view_PauseButton.onBtnPauseClick.AddListener(PauseLevel);

        view_GameMenu = SceneCoreView.GetSceneServiceView<View_GameMenu>();
        view_GameMenu.onBtnQuitGameClick.AddListener(QuitGame);
        view_GameMenu.onBtnRestartClick.AddListener(RestartLevel);
        view_GameMenu.onBtnCloseMenuClick.AddListener(ResumeLevel);

        view_Heart = SceneCoreView.GetSceneServiceView<View_Heart>();
        view_Heart.SetRemainingHearts(playerHealth);

        // Initialize level settings, such as number of enemies
        if (enemyManager == null)
            Debug.LogError("EnemyManager is not assigned or not found in the scene.");
        else
        {
            numberOfEnemies = enemyManager.GetEnemyCount();
        }

        levelTimerCoroutine = StartCoroutine(StartLevelTimer());
        audioManager.PlayMusic(musicCode);

        onLevelStarted?.Invoke();
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
        view_Heart.SetRemainingHearts(playerHealth);
        if (playerHealth <= 0)
        {
            Debug.Log("Player has been defeated!");
            StopCoroutine(levelTimerCoroutine);
            EndLevel(false);
        }
    }

    private void EndLevel(bool isSuccess)
    {
        view_GameResult.SetPerfectHit(enemiesKilled);
        view_GameResult.SetMissedHit(numberOfEnemies - enemiesKilled);
        view_GameResult.Show();

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

        // var totalScore = CalculateScore();
        // Debug.Log($"Total Score: {totalScore}");

        var percentage = GetPercentage();
        Debug.Log($"Completion Percentage: {percentage}%");

        var stars = percentage switch
        {
            >= 100 => 3,
            >= 70 => 2,
            >= 50 => 1,
            _ => 0
        };
        view_GameResult.ShowStars(stars);
        Debug.Log($"Stars Earned: {stars}");

        view_GameResult.SetScore(percentage);

        onLevelEnd.Invoke();
        audioManager.StopMusic();
        PlayerControls.player.SetIdle();

        inputManager.CanInput = false;
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

    private void RestartLevel()
    {
        SceneCore.DestroySceneCore();

        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(activeScene.name);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
