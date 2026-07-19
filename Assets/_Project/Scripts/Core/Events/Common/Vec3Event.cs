using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Vector3 Channel", menuName = "Data/Events/Common/Vector3 Event")]
public class Vector3EventSO : BaseGameEventSO<Vector3> { }
[Serializable] public class Vector3UnityEvent : UnityEvent<Vector3> { }
public class Vector3EventListener : BaseGameEventListener<Vector3, Vector3EventSO, Vector3UnityEvent> { }
