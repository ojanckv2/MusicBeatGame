using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneCore : MonoBehaviour
{
    public static SceneCore Instance;

    [SerializeField] private SceneCoreView sceneCoreView;
    [SerializeField] private List<SceneService> sceneServices;

    private void Awake()
    {
        Instance = this;
        FetchSceneServices();

        Initialize();
        Activate();
    }

    private void Initialize()
    {
        foreach (var service in sceneServices)
        {
            service.Initialize();
        }
    }

    private void DeInitialize()
    {
        foreach (var service in sceneServices)
        {
            service.DeInitialize();
        }
    }

    private void Activate()
    {
        foreach (var service in sceneServices)
        {
            service.Activate();
        }
    }

    private void Deactivate()
    {
        foreach (var service in sceneServices)
        {
            service.Deactivate();
        }
    }

    private void Destroy()
    {
        Deactivate();
        DeInitialize();
    }

    public void FetchSceneServices(bool includeInactive = false)
    {
        sceneServices = transform.GetComponentsInChildren<SceneService>(includeInactive).ToList();

        if (sceneCoreView != null)
            sceneServices.Add(sceneCoreView);
    }

    public static T GetService<T>() where T : SceneService
    {
        if (CheckIfInstanceExists() == false)
            return null;

        return Instance.sceneServices.FirstOrDefault(service => service is T) as T;
    }

    public static SceneCoreView GetSceneCoreView()
    {
        if (CheckIfInstanceExists() == false)
            return null;

        return Instance.sceneCoreView;
    }

    public static void DestroySceneCore()
    {
        if (CheckIfInstanceExists() == false)
            return;

        Instance.Destroy();
    }

    private static bool CheckIfInstanceExists()
    {
        if (Instance == null)
            Debug.LogError("SceneCore instance is not initialized.");

        return Instance != null;
    }
}
