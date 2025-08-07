using UnityEngine;
using UnityEngine.Events;

public class EnvironmentAnimator : MonoBehaviour
{
    private LevelManager levelManager;
    private bool isPaused = true;

    [Header("Beat Settings")]
    public Vector3 direction = Vector3.back; // Movement direction
    private float speed; // Final speed (units/second)

    [SerializeField] private Vector3 resetPosition;
    [SerializeField] private EnvironmentAnimation[] animations = new EnvironmentAnimation[] { };

    [HideInInspector] public UnityEvent<EnvironmentAnimation> onExitedArea = new();

    private void Awake()
    {
        foreach (var animation in animations)
            animation.SetAnimator(this);

        onExitedArea.AddListener(ResetPosition);
    }

    private void Start()
    {
        levelManager = SceneCore.GetService<LevelManager>();
        levelManager.onLevelStarted.AddListener(StartAnimator);
        levelManager.onLevelResumed.AddListener(StartAnimator);
        levelManager.onLevelPaused.AddListener(PauseAnimator);
        levelManager.onLevelEnd.AddListener(PauseAnimator);

        float secondsPerBeat = 60f / levelManager.BPM;
        speed = levelManager.DistancePerBeat / secondsPerBeat;
    }

    private void Update()
    {
        if (isPaused) return;

        foreach (var animation in animations)
        {
            animation.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    private void StartAnimator()
    {
        isPaused = false;
    }

    private void PauseAnimator()
    {
        isPaused = true;
    }

    private void ResetPosition(EnvironmentAnimation animation)
    {
        animation.transform.localPosition = resetPosition;
    }
}