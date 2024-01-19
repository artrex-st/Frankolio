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
    private List<AsyncOperation> _scenesToLoading = new();
    private Stack<ScreenReference> _loadedScenes = new();
    private IEventsService _eventsService;
    private float _fakeLoadTime;

    public void Initialize(ScreenReference startupScreen, ScreenReference loadingScreen, float fakeLoadingTime)
    {
        _loadingScreen = loadingScreen;
        _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
        _loadedScenes.Push(startupScreen);
        _fakeLoadTime = fakeLoadingTime;
    }

    public void LoadingSceneAsync(ScreenReference sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(_loadingScreen.SceneName, LoadSceneMode.Additive);

        loading.completed += async operation =>
        {
            _scenesToLoading.Add(SceneManager.LoadSceneAsync(sceneName.SceneName, LoadSceneMode.Additive));

            foreach (ScreenReference screenReference in _loadedScenes)
            {
                _scenesToLoading.Add(SceneManager.UnloadSceneAsync(screenReference.SceneName));
            }

            _loadedScenes.Clear();
            _loadedScenes.Push(sceneName);
            await StartLoadingProgressAsync();
        };
    }

    private async UniTask StartLoadingProgressAsync()
    {
        float totalSceneProgress = 0;

        do
        {
            foreach (AsyncOperation operation in _scenesToLoading)
            {
                totalSceneProgress += operation.progress;
            }

            //_loadBar.value = totalSceneProgress;
            //totalSceneProgress /= _scenesToLoading.Count;
            totalSceneProgress = Mathf.Clamp01((totalSceneProgress / _scenesToLoading.Count) / .9f);
            _eventsService.Invoke(new ResponseLoadingPercentEvent(totalSceneProgress));
            Debug.Log($"Loading... {totalSceneProgress} / {_scenesToLoading.Count}");
            await UniTask.Delay(TimeSpan.FromSeconds(_fakeLoadTime));
        }
        while (totalSceneProgress < .9f);

        await SceneManager.UnloadSceneAsync(_loadingScreen.SceneName);
        _scenesToLoading.Clear();
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