using UnityEngine;
using UnityEngine.Events;

public class View_PauseButton : SceneServiceView
{
    [SerializeField] private ButtonImproved btnPause;
    public UnityEvent onBtnPauseClick = new();

    protected override void OnActivate()
    {
        btnPause.onPostClick.AddListener(ClickBtnPause);
    }

    private void ClickBtnPause()
    {
        onBtnPauseClick?.Invoke();
    }
}