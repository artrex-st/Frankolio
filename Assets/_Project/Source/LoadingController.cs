using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class LoadingController : BaseScreen
    {
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TextMeshProUGUI _textLoadingPercent;
        private CancellationTokenSource _cancellationTokenSource = new();

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private new void Initialize()
        {
            base.Initialize();

            //_cancelButton.onClick.AddListener(CancelLoadingOperationClickHandler);
            EventsService.Subscribe<ResponseLoadingPercentEvent>(HandlerResponseLoadingPercentEvent);

            // CancellationToken cancellationToken = _cancellationTokenSource.Token;
            //
            // try
            // {
            //     bool result = await SimulateAsyncOperationAsync(cancellationToken);
            //
            //     Debug.Log($"Operação concluída com sucesso? {!result}");
            // }
            // catch (OperationCanceledException)
            // {
            //     Debug.Log("Operação cancelada por exceção.");
            // }
            // finally
            // {
            //     ScreenService.UnLoadAdditiveSceneAsync(_thisScreenRef);
            // }
        }

        // private async UniTask<bool> SimulateAsyncOperationAsync(CancellationToken cancellationToken)
        // {
        //     //await UniTask.Delay(TimeSpan.FromSeconds(10), cancellationToken: cancellationToken);
        //     cancellationToken.ThrowIfCancellationRequested();
        //
        //     return cancellationToken.IsCancellationRequested;
        // }

        // private void CancelLoadingOperationClickHandler()
        // {
        //     _cancellationTokenSource.Cancel();
        // }

        private void HandlerResponseLoadingPercentEvent(ResponseLoadingPercentEvent e)
        {
            _textLoadingPercent.text = $"Loading {e.Percent * 100}%";
        }

        private new void Dispose()
        {
            base.Dispose();
            EventsService.Unsubscribe<ResponseLoadingPercentEvent>(HandlerResponseLoadingPercentEvent);
        }
    }
}
