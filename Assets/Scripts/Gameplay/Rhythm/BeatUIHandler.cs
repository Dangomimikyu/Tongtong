using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using DG.Tweening;

public class BeatUIHandler : MonoBehaviour
{
	[Header("Object references")]
	public Sprite regularBeatOverlay;
	public Sprite feverBeatOverlay;
	public Image overlayRenderer;
	[SerializeField]
	private Canvas m_beatUICanvas;
	[SerializeField]
	private GameObject m_beatTickPrefab;

	[Header("Overlay settings")]
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float fadeSpeed = 0.45f;

	[Header("Combo variables")]
	[SerializeField]
	private int m_comboCount;

	#region Monobehaviour functions
	private void Start()
	{
		overlayRenderer.sprite = regularBeatOverlay;
		Color currentColour = overlayRenderer.color;
		overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 0.0f);

		//EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, SpawnTick);

		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, RenderOutline);
		EventManager.instance.StartListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
	}
	#endregion

	#region Coroutines
	private IEnumerator Fade()
	{
		while (overlayRenderer.color.a > 0)
		{
			Color currentColour = overlayRenderer.color;
			var newAlpha = overlayRenderer.color.a - fadeSpeed * Time.deltaTime;
			overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, newAlpha);
			yield return null;
		}
		yield break;
	}
	#endregion

	#region Beat Tick functions
	private void SpawnTick(EventArgumentData ead)
	{
		float beatDuration = (float)ead.eventParams[0];
		//GameObject tick = Instantiate(m_beatTickPrefab, m_beatUICanvas.transform);
		if (m_beatUICanvas == null)
		{
			Debug.Log("what the fuck");
			return;
		}
		GameObject tick1 = Instantiate(m_beatTickPrefab, m_beatUICanvas.transform);
		tick1.GetComponent<BeatTickBehaviour>().Move(beatDuration * 3f, true);
		GameObject tick2 = Instantiate(m_beatTickPrefab, m_beatUICanvas.transform);
		tick2.GetComponent<BeatTickBehaviour>().Move(beatDuration * 3f, false);
	}
	#endregion

	#region Beat outline UI functions
	public void RenderOutline(EventArgumentData ead)
	{
		if (overlayRenderer == null)
		{
			//Debug.Log("overlay is null 1");
			return;
		}

		Color currentColour = overlayRenderer.color;
		overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 1.0f);
		//StartCoroutine(Fade());
		FadeImage((float)ead.eventParams[0]);
	}

	private void FadeImage(float beatDuration)
	{
		//Debug.Log("call");
		if (overlayRenderer == null)
		{
			Debug.Log("overlay is null 2");
			return;
		}

		Color currentColour = overlayRenderer.color;
		//overlayRenderer.DOColor(new Color(currentColour.r, currentColour.g, currentColour.b, 0), 1);
		overlayRenderer.DOFade(0, beatDuration * 0.5f);
	}

	private void ChangeOverlay(EventArgumentData ead)
	{
		bool fever = ead.eventName == GameEvents.Gameplay_ComboFever ? true : false;

		if (fever)
		{
			overlayRenderer.sprite = feverBeatOverlay;
		}
		else
		{
			overlayRenderer.sprite = regularBeatOverlay;
		}
	}
	#endregion
}
