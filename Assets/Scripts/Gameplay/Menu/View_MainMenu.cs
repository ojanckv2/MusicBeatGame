using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class View_MainMenu : SceneServiceView
{
    [SerializeField] private ButtonImproved btnPlay;
    [SerializeField] private ButtonImproved btnQuit;
    [SerializeField] private GameObject UIBlocker;

    public UnityEvent onBtnPlayClick = new();
    public UnityEvent onBtnQuitClick = new();

    protected override void OnActivate()
    {
        btnPlay.onPostClick.AddListener(SetBtnPlay);
        btnQuit.onPostClick.AddListener(SetBtnQuit);
    }

    private void SetBtnPlay()
    {
        onBtnPlayClick?.Invoke();
    }

    private void SetBtnQuit()
    {
        onBtnQuitClick?.Invoke();
    }

    public void BlockUI(bool value)
    {
        btnPlay.Interactable = !value;
        UIBlocker.SetActive(value);
    }
}