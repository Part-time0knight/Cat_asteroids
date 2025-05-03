using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreAnimation : MonoBehaviour
{
    public event Action<ScoreAnimation> OnEnd;

    private TMP_Text _text;
    private Color _baseColor;
    private Sequence _sequence;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _baseColor = _text.color;
    }

    public void Show(string value, Vector2 position, float time, float endPositionY)
    {
        _text.rectTransform.anchoredPosition = position;
        ResetText();
        _text.text = value;
        Animate(time, endPositionY);
    }

    private void Animate(float duration, float endPosition)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Join(_text.DOFade(0, duration))
            .Join(_text.rectTransform.DOAnchorPosY(_text.rectTransform.anchoredPosition.y + endPosition, duration))
            .Play().OnKill(Callback);
    }

    private void ResetText()
    {
        _text.color = _baseColor;
        if (_sequence != null)
            _sequence.Kill();
    }

    private void Callback()
        => OnEnd(this);
}
