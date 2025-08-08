using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonImproved : MonoBehaviour
{
    [SerializeField] protected Button button;
    public UnityEvent onPreClick = new();
    public UnityEvent onPostClick = new();
    
    public bool Interactable
    {
        get => button.interactable;
        set => button.interactable = value;
    }

    protected virtual void OnValidate()
    {
        var hasButton = TryGetComponent(out button);
        if (!hasButton)
        {
            Debug.LogWarning("Button component is missing. Creating one automatically...");
            button = gameObject.AddComponent<Button>();
        }
    }

    protected virtual void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        onPreClick?.Invoke();
        onPostClick?.Invoke();
    }
}