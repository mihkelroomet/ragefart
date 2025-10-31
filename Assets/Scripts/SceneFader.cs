using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader I { get; private set; }

    [Header("Settings")]
    [SerializeField] float fadeOutDuration = 0.35f;
    [SerializeField] float fadeInDuration = 0.5f;
    [SerializeField] AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);

    CanvasGroup _cg;
    bool _busy;

    void Awake()
    {
        if (I && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        // Build a minimal overlay if none exists
        Canvas canvas = new GameObject("FaderCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 32767; // above everything
        canvas.gameObject.transform.SetParent(transform, false);

        GameObject imgGO = new GameObject("Black");
        imgGO.transform.SetParent(canvas.transform, false);
        Image img = imgGO.AddComponent<Image>();
        img.raycastTarget = true; // blocks clicks during fade
        img.color = Color.black;
        RectTransform rt = (RectTransform)img.transform;
        rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one; rt.offsetMin = rt.offsetMax = Vector2.zero;

        _cg = imgGO.AddComponent<CanvasGroup>();
        _cg.alpha = 0f;
        _cg.blocksRaycasts = false;

        // Optional: fade in when the first scene starts visible
        StartCoroutine(Fade(0f));
    }

    public void LoadScene(string sceneName) => StartCoroutine(LoadSceneRoutine(sceneName));

    IEnumerator LoadSceneRoutine(string sceneName)
    {
        if (_busy) yield break;
        _busy = true;

        // Fade out
        yield return Fade(1f);

        // Load scene
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (op is { isDone: false }) yield return null;

        // Fade in
        yield return Fade(0f);
        _busy = false;
    }

    IEnumerator Fade(float target)
    {
        float fadeDuration = target > 0.5f ? fadeOutDuration : fadeInDuration;
        _cg.blocksRaycasts = true;
        float start = _cg.alpha, t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float k = curve.Evaluate(Mathf.Clamp01(t / fadeDuration));
            _cg.alpha = Mathf.Lerp(start, target, k);
            yield return null;
        }
        _cg.alpha = target;
        _cg.blocksRaycasts = (target > 0.001f);
    }
}
