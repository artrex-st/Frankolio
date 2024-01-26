using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public readonly struct ResponseLoadingPercentEvent : IEvent
{
    public readonly float Percent;
    public readonly float FakeLoadingTime;

    public ResponseLoadingPercentEvent(float percent, float fakeLoadingTime = 0)
    {
        Percent = percent;
        FakeLoadingTime = fakeLoadingTime;
    }
}
//EventsService.Subscribe<ResponseLoadingPercentEvent>(callBack);
//EventsService.Unsubscribe<ResponseLoadingPercentEvent>(callBack);
//EventsService.Invoke(new ResponseLoadingPercentEvent(5));


public class ScreenService : MonoBehaviour, IScreenService
{
    private ScreenReference _loadingScreen;
    private readonly List<AsyncOperation> _scenesToLoading = new();
    private readonly Stack<ScreenReference> _loadedScenes = new();
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

    public void LoadingScenesAsync(List<ScreenReference> scenesName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(_loadingScreen.SceneName, LoadSceneMode.Additive);

        loading.completed += async operation =>
        {

            foreach (ScreenReference screenReference in _loadedScenes)
            {
                _scenesToLoading.Add(SceneManager.UnloadSceneAsync(screenReference.SceneName));
            }

            _loadedScenes.Clear();

            foreach (ScreenReference screenReference in scenesName)
            {
                _scenesToLoading.Add(SceneManager.LoadSceneAsync(screenReference.SceneName, LoadSceneMode.Additive));
                _loadedScenes.Push(screenReference);
            }

            await StartLoadingProgressAsync();
        };
    }

    private async UniTask StartLoadingProgressAsync()
    {
        float totalSceneProgress = 0;
        float timeInLoading = 0;

        do
        {
            foreach (AsyncOperation operation in _scenesToLoading)
            {
                totalSceneProgress += operation.progress;
            }

            totalSceneProgress = Mathf.Clamp01((totalSceneProgress / _scenesToLoading.Count) / .9f);
            new ResponseLoadingPercentEvent(totalSceneProgress).Invoke();
            await UniTask.Yield();
            timeInLoading += Time.deltaTime;
        }
        while (totalSceneProgress < .99f );

        new ResponseLoadingPercentEvent(totalSceneProgress).Invoke();

        if (timeInLoading < _fakeLoadTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_fakeLoadTime));
        }

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
