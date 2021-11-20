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

	[Header("Overlay settings")]
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float fadeSpeed = 0.45f;

	[Header("Combo variables")]
	[SerializeField]
	private int m_comboCount;

	~BeatUIHandler()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, RenderOutline);
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
	}

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
