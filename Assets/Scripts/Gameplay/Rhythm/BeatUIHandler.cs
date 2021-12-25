using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using DG.Tweening;

// macros
using cmdInput = CommandAtrributes.Inputs;

public class BeatUIHandler : MonoBehaviour
{
	[Header("Sprite references")]
	public Sprite regularBeatOverlay;
	public Sprite feverBeatOverlay;

	[SerializeField]
	private Sprite m_noInputSprite;
	[SerializeField]
	private Sprite m_waitInputSprite;
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
	private bool m_currentWaiting = false;
	private int m_waitBeatCount = 0;

	~BeatUIHandler()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, RenderBeatPulse);
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Input_Drum, EditInputUI);
		EventManager.instance.StopListening(GameEvents.Input_CommandComplete, WaitPostCommand);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, FailedCommand);
	}

	#region Monobehaviour functions
	private void Start()
	{
		overlayRenderer.sprite = regularBeatOverlay;
		Color currentColour = overlayRenderer.color;
		overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 0.0f);

		//EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, SpawnTick);

		EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, RenderBeatPulse);
		EventManager.instance.StartListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
		EventManager.instance.StartListening(GameEvents.Input_Drum, EditInputUI);
		EventManager.instance.StartListening(GameEvents.Input_CommandSuccess, WaitPostCommand);
		EventManager.instance.StartListening(GameEvents.Input_CommandFail, FailedCommand);
	}

	private void OnDisable()
	{
		EventManager.instance.StopListening(GameEvents.Gameplay_MetronomeBeat, RenderBeatPulse);
		EventManager.instance.StopListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
		EventManager.instance.StopListening(GameEvents.Input_Drum, EditInputUI);
		EventManager.instance.StopListening(GameEvents.Input_CommandSuccess, WaitPostCommand);
		EventManager.instance.StopListening(GameEvents.Input_CommandFail, FailedCommand);
	}
	#endregion

	#region Beat Bar UI functions
	public void RenderBeatPulse(EventArgumentData ead)
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
		if (overlayRenderer == null)
		{
			Debug.Log("overlay is null 2");
			return;
		}

		overlayRenderer.DOFade(0.1f, beatDuration * fadeSpeed);
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

	#region Beat Input UI functions
	private void EditInputUI(EventArgumentData ead)
	{
		cmdInput input = (cmdInput)ead.eventParams[0];
		Sprite newSprite = m_noInputSprite; // set the newSprite to be the noInput initially

		// double input or no input
		if (m_inputThisBeat && input != cmdInput.BeatEnd || input == cmdInput.None)
		{
			if (!m_currentWaiting)
			{
				Debug.LogWarning("double or no input");
				ResetBeatInputUI();
				return;
			}
		}

		switch (input)
		{
			// cmdInput.None should only be reached during the waiting time
			case cmdInput.None:
				//if (m_currentBeatCount == 4)
				//{
				//	Debug.LogError("this shouldn't happen"); // [to remove]
				//}
				//else if (!m_inputThisBeat) // player missed this beat
				//{
				//	Debug.Log("player missed this beat. beatCount: " + m_currentBeatCount);
				//	ResetBeatInputUI();
				//}
				//m_inputThisBeat = false; // reset the input bool for next beat

				if (!m_currentWaiting)
				{
					Debug.LogError("AAAAAAAAAA");
				}
				Debug.Log("HERE");
				// newSprite = m_noInputSprite; // [to remove] no need to set newSprite again since it's set already
				break;
			case cmdInput.BeatEnd:
				if (m_currentBeatCount == 4) // player completed this command
				{
					Debug.LogWarning("beat count 4");
					ResetBeatInputUI();
				}
				m_inputThisBeat = false; // reset the input bool for next beat
				return;
			case cmdInput.Walk:
				newSprite = m_leftArrowInputSprite;
				break;
			case cmdInput.Attack:
				newSprite = m_rightArrowInputSprite;
				break;
			case cmdInput.Defend:
				newSprite = m_upArrowInputSprite;
				break;
			case cmdInput.Magic:
				newSprite = m_downArrowInputSprite;
				break;
			default:
				break;
		}

		SetBeatInputUI(newSprite);
	}

	private void WaitPostCommand(EventArgumentData ead)
	{
		m_currentWaiting = true;

		// set all the input UI to be the waiting sprite
		ResetBeatUIWait();
	}

	private void FailedCommand(EventArgumentData ead)
	{
		Debug.LogWarning("failed command");
		ResetBeatInputUI();
	}

	private void ResetBeatInputUI()
	{
		Debug.Log("reset beat input ui");
		foreach (Image i in inputDisplayList)
		{
			i.sprite = m_noInputSprite;
		}
		m_currentBeatCount = 0;
		m_waitBeatCount = 0;
		m_currentWaiting = false;
		m_inputThisBeat = false;
	}

	private void ResetBeatUIWait()
	{
		// set all the beat input UI to the waiting icon
		foreach (Image i in inputDisplayList)
		{
			i.sprite = m_waitInputSprite;
		}
		m_waitBeatCount = 0;
		m_currentBeatCount = 0;
		m_inputThisBeat = false;
	}

	private void SetBeatInputUI(Sprite newSprite)
	{
		if (m_currentWaiting)
		{
			Debug.Log("wait count size: " + m_waitBeatCount);
			inputDisplayList[m_waitBeatCount].sprite = newSprite;
			m_waitBeatCount++;

			if (m_waitBeatCount >= 4)
			{
				Debug.Log("finished waiting");
				m_currentWaiting = false;
				m_waitBeatCount = 0;
				ResetBeatInputUI();
			}
		}
		else
		{
			inputDisplayList[m_currentBeatCount].sprite = newSprite;
			m_currentBeatCount++;
		}
		m_inputThisBeat = true;
	}
	#endregion
}
