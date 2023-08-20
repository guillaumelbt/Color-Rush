using UnityEngine;

[CreateAssetMenu(menuName = "Volume Scriptable")]
public class VolumeScriptable : ScriptableObject
{
    public float globalVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;
}
