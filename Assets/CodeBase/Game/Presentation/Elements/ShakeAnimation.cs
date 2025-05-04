using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.Elements
{
    public class ShakeAnimation : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private Vector2 _basePosition;
        private Tweener _tweener;

        private void Awake()
        {
            _basePosition = _settings.Container.anchoredPosition;
        }

        public void Play(Action callback = null)
        {
            if (_tweener != null && _tweener.active)
                _tweener.Kill();

            List<ShakeObject> sItems = new();
            foreach (var image in _settings.Container.GetComponentsInChildren<Image>())
            {
                ShakeImage sImage = new(image);
                sItems.Add(sImage);
                sImage.Color = _settings.Color;
            }

            foreach (var text in _settings.Container.GetComponentsInChildren<TMP_Text>())
            {
                ShakeText sText = new(text);
                sItems.Add(sText);
                sText.Color = _settings.Color;
            }


            TweenCallback onKill = () =>
            {
                foreach (var sItem in sItems)
                    sItem.Reset();
                _settings.Container.anchoredPosition = _basePosition;
                callback?.Invoke();
            };

            _tweener = _settings.Container.DOShakeAnchorPos(_settings.Duration, randomnessMode: ShakeRandomnessMode.Full).OnKill(onKill);
        }

        public abstract class ShakeObject
        {
            public virtual Color BaseColor { get; protected set; }
            public virtual Color Color { get; set; }

            public virtual void Reset()
                => Color = BaseColor;
        }

        public class ShakeText : ShakeObject
        {
            public TMP_Text Text { get; private set; }

            public override Color Color
            {
                get => Text.color;
                set => Text.color = value;
            }

            public ShakeText(TMP_Text text) 
            {
                Text = text;
                BaseColor = text.color;
            }
        }

        public class ShakeImage : ShakeObject
        {
            public Image Image { get; private set; }

            public override Color Color
            {
                get => Image.color;
                set => Image.color = value;
            }

            public ShakeImage(Image image)
            {
                Image = image;
                BaseColor = image.color;
            }
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Color Color { get; private set; }
            [field: SerializeField] public float Duration { get; private set; }
            [field: SerializeField] public RectTransform Container { get; private set; }
        }
    }
}