using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Float Channel", menuName = "Events/Float CHannel")]
public class FloatEventSO : BaseGameEventSO<float> { }
[Serializable] public class FloatUnityEvent : UnityEvent<float> { }
public class FloatEventListener : BaseGameEventListener<float, FloatEventSO, FloatUnityEvent> { }
