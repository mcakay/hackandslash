using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Bool Channel", menuName = "Events/Bool Channel")]
public class BoolEventSO : BaseGameEventSO<bool> { }
[Serializable] public class BoolUnityEvent : UnityEvent<bool> { }
public class BoolEventListener : BaseGameEventListener<bool, BoolEventSO, BoolUnityEvent> { }
