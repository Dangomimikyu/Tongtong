using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DangoMimikyu.EventManagement;
using DG.Tweening;

public class BeatUIHandler : MonoBehaviour
{
    [Header("Overlay references")]
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

    #region Monobehaviour functions
    private void Start()
    {
        overlayRenderer.sprite = regularBeatOverlay;
        Color currentColour = overlayRenderer.color;
        overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 0.0f);

        //EventManager.instance.StartListening(GameEvents.Gameplay_MetronomeBeat, RenderOutline);
        //EventManager.instance.StartListening(GameEvents.Gameplay_ComboFever, ChangeOverlay);
        //EventManager.instance.StartListening(GameEvents.Gameplay_BreakCombo, ChangeOverlay);
    }

	private void Update()
	{
		if (overlayRenderer == null)
		{
            Debug.Log("overlay null");
		}
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

    #region Beat outline UI functions
    public void RenderOutline(EventArgumentData ead)
    {
        if (overlayRenderer == null)
        {
            Debug.Log("overlay is null 1");
            return;
        }

        Color currentColour = overlayRenderer.color;
        overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 1.0f);
        //StartCoroutine(Fade());
        FadeImage();
    }

    private void FadeImage()
	{
        Color currentColour = overlayRenderer.color;
        overlayRenderer.DOColor(new Color(currentColour.r, currentColour.g, currentColour.b, 0), 1);
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
            //overlayRenderer.sprite = regularBeatOverlay;
		}
	}
    #endregion
}
