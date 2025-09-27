using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class show_message : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject firstImage;
    public GameObject secondImage;
    public Text text_1;
    public Text press_E_to;
    public Text text_2;

    [Header("Animation Settings")]
    public float scaleDuration = 0.3f;
    public float fadeDuration = 0.3f;
    public float delayBetween = 0.25f;
    public float typingSpeed = 0.03f;
    public float pulseAmplitude = 0.04f;
    public float pulseSpeed = 5f;

    private bool waitingForInput = false;
    private bool text2Shown = false;
    private Coroutine pulseCoroutine;

    void Start()
    {
        if (firstImage != null) firstImage.SetActive(false);
        if (secondImage != null)
        {
            secondImage.SetActive(false);
            AddCanvasGroup(secondImage);
        }
        if (text_1 != null) { text_1.gameObject.SetActive(false); AddCanvasGroup(text_1.gameObject); }
        if (press_E_to != null) { press_E_to.gameObject.SetActive(false); AddCanvasGroup(press_E_to.gameObject); }
        if (text_2 != null) { text_2.gameObject.SetActive(false); AddCanvasGroup(text_2.gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (firstImage != null)
            {
                firstImage.SetActive(true);
                firstImage.transform.localScale = Vector3.zero;
                StartCoroutine(ScaleUp(firstImage.transform, scaleDuration));
            }

            if (secondImage != null)
                Invoke("ShowSecondImage", delayBetween);
        }
    }

    void ShowSecondImage()
    {
        secondImage.SetActive(true);
        secondImage.transform.localScale = Vector3.zero;
        StartCoroutine(ScaleAndFadeIn(secondImage, scaleDuration, fadeDuration, OnSecondImageShown));
    }

    void OnSecondImageShown()
    {
        if (text_1 != null)
        {
            text_1.gameObject.SetActive(true);
            StartCoroutine(TypewriterEffect(text_1, text_1.text, OnText1Complete));
        }
    }

    void OnText1Complete()
    {
        if (press_E_to != null)
        {
            press_E_to.gameObject.SetActive(true);
            StartCoroutine(FadeInCanvas(press_E_to.gameObject, fadeDuration));
            StartPulse(press_E_to.transform);
        }
        waitingForInput = true;
    }

    void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.E))
        {
            if (!text2Shown)
            {
                // Primera pulsación de E: mostrar text_2
                waitingForInput = false;

                if (text_1 != null) text_1.gameObject.SetActive(false);
                StopPulse();

                if (text_2 != null)
                {
                    text_2.gameObject.SetActive(true);
                    StartCoroutine(TypewriterEffect(text_2, text_2.text, OnText2Complete));
                }

                text2Shown = true;
            }
            else
            {
                // Segunda pulsación de E: cerrar todo
                CloseAll();
            }
        }
    }

    void OnText2Complete()
    {
        if (press_E_to != null)
        {
            press_E_to.gameObject.SetActive(true);
            StartCoroutine(FadeInCanvas(press_E_to.gameObject, fadeDuration));
            StartPulse(press_E_to.transform);
        }
        waitingForInput = true;
    }

    void CloseAll()
    {
        waitingForInput = false;
        StopPulse();

        if (firstImage != null) firstImage.SetActive(false);
        if (secondImage != null) secondImage.SetActive(false);
        if (text_1 != null) text_1.gameObject.SetActive(false);
        if (text_2 != null) text_2.gameObject.SetActive(false);
        if (press_E_to != null) press_E_to.gameObject.SetActive(false);

        text2Shown = false;
    }

    #region Animations
    IEnumerator ScaleUp(Transform target, float duration)
    {
        Vector3 initial = Vector3.zero;
        Vector3 finalScale = Vector3.one;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, timer / duration);
            target.localScale = Vector3.Lerp(initial, finalScale, t);
            yield return null;
        }
        target.localScale = Vector3.one;
    }

    IEnumerator ScaleAndFadeIn(GameObject obj, float scaleDur, float fadeDur, System.Action onComplete = null)
    {
        Transform t = obj.transform;
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 0f;

        t.localScale = Vector3.zero;
        float timer = 0f;

        while (timer < scaleDur)
        {
            timer += Time.deltaTime;
            float tLerp = Mathf.SmoothStep(0f, 1f, timer / scaleDur);
            t.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, tLerp);

            if (cg != null)
            {
                float alpha = Mathf.Lerp(0f, 1f, timer / fadeDur);
                cg.alpha = Mathf.Clamp01(alpha);
            }
            yield return null;
        }

        t.localScale = Vector3.one;
        if (cg != null) cg.alpha = 1f;

        onComplete?.Invoke();
    }

    IEnumerator FadeInCanvas(GameObject obj, float duration)
    {
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null) yield break;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(timer / duration);
            yield return null;
        }
        cg.alpha = 1f;
    }

    IEnumerator TypewriterEffect(Text uiText, string fullText, System.Action onComplete = null)
    {
        uiText.text = "";
        foreach (char c in fullText)
        {
            uiText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        onComplete?.Invoke();
    }

    void StartPulse(Transform t)
    {
        if (pulseCoroutine != null) StopCoroutine(pulseCoroutine);
        pulseCoroutine = StartCoroutine(PulseText(t));
    }

    void StopPulse()
    {
        if (pulseCoroutine != null) StopCoroutine(pulseCoroutine);
        if (press_E_to != null) press_E_to.transform.localScale = Vector3.one;
    }

    IEnumerator PulseText(Transform t)
    {
        Vector3 original = Vector3.one;
        while (true)
        {
            float scale = 1f + pulseAmplitude * Mathf.Sin(Time.time * pulseSpeed);
            t.localScale = original * scale;
            yield return null;
        }
    }

    void AddCanvasGroup(GameObject obj)
    {
        if (obj.GetComponent<CanvasGroup>() == null)
            obj.AddComponent<CanvasGroup>();
    }
    #endregion
}