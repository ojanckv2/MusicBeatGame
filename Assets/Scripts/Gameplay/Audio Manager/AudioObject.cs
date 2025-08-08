using UnityEngine;

[CreateAssetMenu(fileName = "Audio Object", menuName = "Scriptable Object/Audio/Audio Object")]
public class AudioObject : ScriptableObject
{
    [SerializeField] private string audioCode;
    [SerializeField] private AudioClip audioClip;

    [Range(0f, 1f)]
    [SerializeField] private float maxVolume = 1f;

    public string AudioCode => audioCode;
    public AudioClip AudioClip => audioClip;
    public float MaxVolume => maxVolume;
}
