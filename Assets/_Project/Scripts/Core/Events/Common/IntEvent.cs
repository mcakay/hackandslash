using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Int Channel", menuName = "Data/Events/Common/Int Event")]
public class IntEventSO : BaseGameEventSO<int> { }
[Serializable] public class IntUnityEvent : UnityEvent<int> { }
public class IntEventListener : BaseGameEventListener<int, IntEventSO, IntUnityEvent> { }
