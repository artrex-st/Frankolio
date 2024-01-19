using UnityEngine;


[CreateAssetMenu(menuName = "ScreenService/Screen Reference")]
public sealed class ScreenReference : ScriptableObject
{
    [SerializeField] private string _sceneName;

    public string SceneName => _sceneName;

    public ScreenReference(string sceneName)
    {
        _sceneName = sceneName;
    }
}
