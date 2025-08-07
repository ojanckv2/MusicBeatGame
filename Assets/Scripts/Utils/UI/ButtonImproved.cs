using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonImproved : MonoBehaviour
{
    protected Button button;
    public UnityEvent onPreClick = new();
    public UnityEvent onPostClick = new();

    protected virtual void OnValidate()
    {
        var hasButton = TryGetComponent(out button);
        if (!hasButton) {
            Debug.LogWarning("Button component is missing. Creating one automatically...");
            button = gameObject.AddComponent<Button>();
        }

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        onPreClick?.Invoke();
        onPostClick?.Invoke();
    }
}