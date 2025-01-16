using System.Collections;
using TMPro;
using UnityEngine;

public class DamageHitAmountDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text amounText;
    [SerializeField] private GameObject goToScale;
    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private AnimationCurve scaleAnimCurve;
    [SerializeField] private Vector3 startingScale = Vector3.zero;
    [SerializeField] private Vector3 endingScale = Vector3.one;

    private IEnumerator playingCoroutine;

    void Awake()
    {
        goToScale.SetActive(false);
    }

    public void SetAmount(int amount)
    {
        amounText.text = $"-{amount}";
    }

    public void Play()
    {
        TryToStopRunningCoroutine();

        playingCoroutine = DoPlayAnim();
        StartCoroutine(playingCoroutine);
    }

    private IEnumerator DoPlayAnim()
    {
        // TODO fade

        goToScale.SetActive(true);
        float time = 0f;

        while(time < animDuration)
        {
            time += Time.deltaTime;
            float progress = time / animDuration;

            goToScale.transform.localScale = Vector3.Lerp(startingScale, endingScale, scaleAnimCurve.Evaluate(progress));

            yield return null;
        }
        goToScale.transform.localScale = endingScale;
        goToScale.SetActive(false);
    }

    private void TryToStopRunningCoroutine()
    {
        if (playingCoroutine != null)
        {
            StopCoroutine(playingCoroutine);
        }
    }
}
