using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source
{
    public class LoadingController : BaseScreen
    {
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TextMeshProUGUI _textLoadingPercent;
        [SerializeField] private Slider _loadBar;
        private float _target;
        private const float LoadBarSpeed = 10f;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _loadBar.value = Mathf.Lerp(_loadBar.value, _target, LoadBarSpeed * Time.deltaTime);
            _textLoadingPercent.text = $"Loading {(_loadBar.value * 100):N0}%";
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private new void Initialize()
        {
            base.Initialize();
            //EventsService.Subscribe<ResponseLoadingPercentEvent>(HandlerResponseLoadingPercentEvent);
            new ResponseLoadingPercentEvent().AddListener(HandlerResponseLoadingPercentEvent);

            _loadBar.value = 0;
        }

        private void HandlerResponseLoadingPercentEvent(ResponseLoadingPercentEvent e)
        {
            _target = e.Percent;
        }

        private new void Dispose()
        {
            base.Dispose();
            //EventsService.Unsubscribe<ResponseLoadingPercentEvent>(HandlerResponseLoadingPercentEvent);
            new ResponseLoadingPercentEvent().RemoveListener(HandlerResponseLoadingPercentEvent);
        }
    }
}
