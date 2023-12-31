using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCard : MonoBehaviour
{
    public float Fill { get; set; }
    public bool IsShining
    {
        get => _isShining;
        set
        {
            if (_isShining != value)
            {
                _isShining = value;
                LeanTween.cancel(_crystalShine);
                LeanTween.alpha(_crystalShine, value ? 1f : 0f, 0.2f);
            }
        }
    }

    public void SetProgress(int current, int goal)
    {
        LeanTween.cancel(gameObject);
        var rt = GetComponent<RectTransform>();
        rt.localScale = Vector3.one * 0.95f;
        LeanTween.scale(rt, Vector3.one, 0.125f).setEaseInCubic();
        _text.text = $"{current}/{goal}";
    }

    public void SetBase(Sprite sprite) => _base.sprite = sprite;

    private void Update()
    {
        _smoothFill = Mathf.Lerp(_smoothFill, Fill, Time.deltaTime * 5f);

        SetBarFill(_barDark, _smoothFill);
        _barCap.anchoredPosition = new Vector3(157.5f * _smoothFill, 0);
    }

    private void SetBarFill(RectTransform bar, float fill) => bar.sizeDelta = new Vector2(159 * fill, 22.5f);

    [SerializeField] private Image _base;
    [SerializeField] private RectTransform _barCap;
    [SerializeField] private RectTransform _barDark;
    [SerializeField] private RectTransform _crystalShine;
    [SerializeField] private TMP_Text _text;

    private float _smoothFill;
    private bool _isShining;
}