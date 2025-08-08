using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkill : ButtonImproved
{
    [SerializeField] private Image cooldownImage;
    [SerializeField] private TextMeshProUGUI cooldownText;

    public void ShowCooldown(float cooldownTime)
    {
        cooldownImage.fillAmount = 1f;
        StartCoroutine(StartCooldown(cooldownTime));
    }

    private IEnumerator StartCooldown(float cooldownTime)
    {
        cooldownImage.gameObject.SetActive(true);
        button.interactable = false;

        float elapsed = 0f;
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = Mathf.Clamp01(1f - (elapsed / cooldownTime));
            cooldownText.text = Mathf.CeilToInt(cooldownTime - elapsed).ToString();
            yield return null;
        }
        cooldownImage.fillAmount = 0f;
        
        cooldownImage.gameObject.SetActive(false);
        button.interactable = true;
    }
}