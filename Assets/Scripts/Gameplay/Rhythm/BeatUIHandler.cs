using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BeatUIHandler : MonoBehaviour
{
    [Header("Overlay references")]
    public Sprite regularBeatOverlay;
    public Sprite feverBeatOverlay;
    private Sprite m_currentBeatOverlay;
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
        m_currentBeatOverlay = regularBeatOverlay;
        overlayRenderer.sprite = m_currentBeatOverlay;
        Color currentColour = overlayRenderer.color;
        overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 0.0f);
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
    }
    #endregion

    #region Beat outline UI functions
    public void RenderOutline()
    {
        Color currentColour = overlayRenderer.color;
        overlayRenderer.color = new Color(currentColour.r, currentColour.g, currentColour.b, 1.0f);
        StartCoroutine(Fade());
    }
    #endregion
}
