using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using DG.Tweening;

public class BeatUIHandler : MonoBehaviour
{
	[Header("Sprite references")]
	public Sprite regularBeatOverlay;
	public Sprite feverBeatOverlay;

	[SerializeField]
	private Sprite m_noInputSprite;
	[SerializeField]
	private Sprite m_leftArrowInputSprite;
	[SerializeField]
	private Sprite m_rightArrowInputSprite;
	[SerializeField]
	private Sprite m_upArrowInputSprite;
	[SerializeField]
	private Sprite m_downArrowInputSprite;

	[Header("Image references")]
	public Image overlayRenderer;
	[SerializeField]
	public List<Image> inputDisplayList;

	[Header("Overlay settings")]
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float fadeSpeed = 0.35f;

	[Header("Combo variables")]
	[SerializeField]
	private int m_comboCount;

	private int m_currentBeatCount = 0;
	private bool m_inputThisBeat = false;

	~BeatUIHandler()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, RenderOutline);
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Input_Drum, EditInputUI);
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
		EventManager.instance.StartListening(GameEvents.Input_Drum, EditInputUI);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, RenderOutline);
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Input_Drum, EditInputUI);
	}
	#endregion

	#region Beat Bar UI functions
	public void RenderOutline(EventArgumentData ead)
	{
		if (overlayRenderer == null)
		{
			Debug.Log("overlay is null 1");
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
		overlayRenderer.DOFade(0.1f, beatDuration * fadeSpeed);
	}

	private void ChangeOverlay(EventArgumentData ead)
	{
		bool fever = ead.eventName == GameEvents.Gameplay_ComboFever ? true : false;

		if (fever)
		{
			overlayRenderer.sprite = feverBeatOverlay;
			//overlayRenderer.color = new Color(1f, 0f, 0f);
		}
		else
		{
			overlayRenderer.sprite = regularBeatOverlay;
			//overlayRenderer.color = new Color(0.1084906f, 1f, 0.7643049f);
		}

		//ResetBeatInputUI();
	}
	#endregion

	#region Beat Input UI functions
	private void EditInputUI(EventArgumentData ead)
	{
		CommandAtrributes.Inputs input = (CommandAtrributes.Inputs)ead.eventParams[0];
		Sprite newSprite = m_noInputSprite; // set the newSprite to be the noInput initially

		// check for double input
		if (m_inputThisBeat && input != CommandAtrributes.Inputs.None)
		{
			ResetBeatInputUI();
		}

		switch (input)
		{
			case CommandAtrributes.Inputs.None:
				if (!m_inputThisBeat || m_currentBeatCount == 4) // player missed this beat
				{
					ResetBeatInputUI();
				}
				m_inputThisBeat = false; // reset the input bool for next beat
				return;
			case CommandAtrributes.Inputs.Walk:
				newSprite = m_leftArrowInputSprite;
				break;
			case CommandAtrributes.Inputs.Attack:
				newSprite = m_rightArrowInputSprite;
				break;
			case CommandAtrributes.Inputs.Defend:
				newSprite = m_upArrowInputSprite;
				break;
			case CommandAtrributes.Inputs.Magic:
				newSprite = m_downArrowInputSprite;
				break;
			default:
				break;
		}

		SetBeatInputUI(newSprite);
	}

	private void ResetBeatInputUI()
	{
		Debug.Log("RESETTING");
		foreach (Image i in inputDisplayList)
		{
			i.sprite = m_noInputSprite;
		}
		m_currentBeatCount = 0;
		m_inputThisBeat = false;
	}

	private void SetBeatInputUI(Sprite newSprite)
	{
		inputDisplayList[m_currentBeatCount].sprite = newSprite;
		m_currentBeatCount++;
		m_inputThisBeat = true;
	}
	#endregion
}
