using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public readonly struct ResponseLoadingPercentEvent : IEvent
{
    public readonly float Percent;

    public ResponseLoadingPercentEvent(float percent)
    {
        Percent = percent;
    }
}
//EventsService.Subscribe<ResponseLoadingPercentEvent>(callBack);
//EventsService.Unsubscribe<ResponseLoadingPercentEvent>(callBack);
//EventsService.Invoke(new ResponseLoadingPercentEvent(5));


public class ScreenService : MonoBehaviour, IScreenService
{
    private ScreenReference _loadingScreen;
    private List<AsyncOperation> _scenesLoading = new();
    private Stack<ScreenReference> _loadedScenes = new();
    private IEventsService _eventsService;

    public void Initialize(ScreenReference loadingScreen)
    {
        _loadingScreen = loadingScreen;
        _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
    }

    public void LoadingSceneAsync(ScreenReference sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(_loadingScreen.SceneName, LoadSceneMode.Additive);

        loading.completed += async operation =>
        {
            //await Task.Delay(TimeSpan.FromSeconds(2));

            foreach (ScreenReference screenReference in _loadedScenes)
            {
                _scenesLoading.Add(SceneManager.UnloadSceneAsync(screenReference.SceneName));
            }

            _scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName.SceneName));
            _loadedScenes.Clear();
            _loadedScenes.Push(sceneName);
            StartLoadingProgressAsync();
            SceneManager.UnloadSceneAsync(_loadingScreen.SceneName);
        };
    }

    private async UniTask StartLoadingProgressAsync()
    {
        float totalSceneProgress;

        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (AsyncOperation operation in _scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                //_loadBar.value = totalSceneProgress;
                _eventsService.Invoke(new ResponseLoadingPercentEvent(totalSceneProgress));
                //totalSceneProgress = (totalSceneProgress / _scenesLoading.Count) * 100f;
                //_loadBarText.text = $"Loading ... {Mathf.RoundToInt(totalSceneProgress)}%";
                Debug.Log($"Loading... ");
                await UniTask.Yield();
            }
        }

        await UniTask.Delay(TimeSpan.FromSeconds(2));
        _scenesLoading.Clear();
    }

    public void LoadingSceneAdditiveAsync(ScreenReference sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName.SceneName, LoadSceneMode.Additive);
        _loadedScenes.Push(sceneName);
    }

    public void UnLoadAdditiveSceneAsync(ScreenReference sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName.SceneName);
        _loadedScenes.Pop();
    }
}
