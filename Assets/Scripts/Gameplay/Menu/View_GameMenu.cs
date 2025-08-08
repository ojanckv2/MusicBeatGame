using UnityEngine;
using UnityEngine.Events;

public class View_GameMenu : SceneServiceView
{
    [SerializeField] private ButtonImproved btnRestartLevel;
    [SerializeField] private ButtonImproved btnQuitGame;
    [SerializeField] private ButtonImproved btnCloseMenu;

    public UnityEvent onBtnRestartClick = new();
    public UnityEvent onBtnQuitGameClick = new();
    public UnityEvent onBtnCloseMenuClick = new();

    protected override void OnActivate()
    {
        btnRestartLevel.onPostClick.AddListener(SetBtnRestart);
        btnQuitGame.onPostClick.AddListener(SetBtnQuitGame);
        btnCloseMenu.onPostClick.AddListener(SetBtnCloseMenu);
    }

    private void SetBtnRestart()
    {
        onBtnRestartClick?.Invoke();
    }

    private void SetBtnQuitGame()
    {
        onBtnQuitGameClick?.Invoke();
    }

    private void SetBtnCloseMenu()
    {
        onBtnCloseMenuClick?.Invoke();
        Hide();
    }
}
