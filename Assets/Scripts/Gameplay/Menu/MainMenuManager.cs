using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : SceneService
{
    private View_MainMenu view_MainMenu;

    protected override void OnActivate()
    {
        view_MainMenu = SceneCoreView.GetSceneServiceView<View_MainMenu>();
        view_MainMenu.onBtnPlayClick.AddListener(GoToMainGame);
        view_MainMenu.onBtnQuitClick.AddListener(QuitGame);
    }

    private void GoToMainGame()
    {
        view_MainMenu.BlockUI(true);
        SceneCore.DestroySceneCore();
        SceneManager.LoadSceneAsync("MainGame");
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