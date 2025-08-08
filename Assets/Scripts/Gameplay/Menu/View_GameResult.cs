using System.Collections;
using Lean.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class View_GameResult : SceneServiceView
{
    [SerializeField] private Transform parentStars;
    [SerializeField] private GameObject prefabStar;

    [SerializeField] private TextMeshProUGUI textPerfectHitAmount;
    [SerializeField] private TextMeshProUGUI textMissedHitAmount;
    [SerializeField] private TextMeshProUGUI textScoreAmount;

    [SerializeField] private CanvasGroup canvasGroupButtons;
    [SerializeField] private ButtonImproved btnRestart;
    [SerializeField] private ButtonImproved btnQuit;

    public UnityEvent onBtnRestartClick = new();
    public UnityEvent onBtnQuitClick = new();

    protected override void OnActivate()
    {
        btnRestart.onPostClick.AddListener(ClickBtnRestart);
        btnQuit.onPostClick.AddListener(ClickBtnQuit);
    }

    private void ClickBtnRestart()
    {
        onBtnRestartClick?.Invoke();
    }

    private void ClickBtnQuit()
    {
        onBtnQuitClick?.Invoke();
    }

    public void ShowStars(int amount)
    {
        if (amount == 0) return;

        StartCoroutine(AnimateStars(amount));
    }

    public void SetScore(float score)
    {
        StartCoroutine(AnimateScore(score));
    }

    public void SetPerfectHit(int amount)
    {
        textPerfectHitAmount.text = amount.ToString();
    }

    public void SetMissedHit(int amount)
    {
        textMissedHitAmount.text = amount.ToString();
    }

    private IEnumerator AnimateStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            LeanPool.Spawn(prefabStar, parentStars);
            yield return new WaitForSeconds(.75f);
        }
    }

    private IEnumerator AnimateScore(float score)
    {
        float displayedScore = 0;
        float duration = 1.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            displayedScore = Mathf.Lerp(0, score, elapsed / duration);
            textScoreAmount.text = displayedScore.ToString("00.00");
            yield return null;
        }

        textScoreAmount.text = score.ToString("00.00");

        yield return FadeInContainerButton();
    }

    private IEnumerator FadeInContainerButton()
    {
        var duration = 0.25f;
        var endValue = 1f;
        var startValue = 0f;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            var t = Mathf.Clamp01(elapsed / duration);
            canvasGroupButtons.alpha = Mathf.Lerp(startValue, endValue, t);
            yield return null;
        }
        canvasGroupButtons.alpha = endValue;

        canvasGroupButtons.interactable = true;
        canvasGroupButtons.blocksRaycasts = true;
        onShow?.Invoke();
    }
}