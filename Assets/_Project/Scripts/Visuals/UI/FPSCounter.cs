using UnityEngine;
using TMPro;
using Cysharp.Text;

public class FPSCounter : MonoBehaviour
{
	[Header("Settings")]
	public float PollingTime = 0.5f;

	[SerializeField] private TextMeshProUGUI _fpsText;

	private float _time;
	private int _frameCount;
	private float _worstFrameTime;

	private void Awake()
	{
		QualitySettings.vSyncCount = 0;

		Application.targetFrameRate = 60;
	}

	private void Update()
	{
		float currentDelta = Time.unscaledDeltaTime;

		_time += currentDelta;
		_frameCount++;

		if (currentDelta > _worstFrameTime)
		{
			_worstFrameTime = currentDelta;
		}

		if (_time >= PollingTime)
		{
			int fps = Mathf.RoundToInt(_frameCount / _time);
			float worstMs = _worstFrameTime * 1000f;

			_fpsText.text = ZString.Format("{0}\n{1:F1} ms", fps, worstMs);

			int targetFps = Application.targetFrameRate > 0 ? Application.targetFrameRate : 60;
			float targetMs = 1000f / targetFps;

			if (worstMs > targetMs * 1.15f)
				_fpsText.color = Color.red;
			else if (fps >= targetFps * 0.95f)
				_fpsText.color = Color.green;
			else
				_fpsText.color = Color.yellow;

			_time -= PollingTime;
			_frameCount = 0;
			_worstFrameTime = 0f;
		}
	}
}
