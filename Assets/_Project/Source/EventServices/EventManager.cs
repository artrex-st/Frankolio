using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private Button _debugEventButton;
    [SerializeField] private string _eventText;
    private IEventsService _eventsService;

    private void Start()
    {
        _debugEventButton.onClick.AddListener(DebugEventClickHandler);
        _eventsService = ServiceLocator.Instance.GetService<IEventsService>();
    }

    private void DebugEventClickHandler()
    {
        //TODO: DEBUG EVENT TEST
        //_eventsService.Invoke(new RequestStringEvent(_eventText));
    }
}
