using TMPro;
using UnityEngine;

public class View_Heart : SceneServiceView
{
    [SerializeField] private TextMeshProUGUI textRemainingHeart;

    public void SetRemainingHearts(int hearts)
    {
        textRemainingHeart.text = hearts.ToString();
    }
}
