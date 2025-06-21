using DG.Tweening;
using UnityEngine;

namespace Game.Logic.Player.Animation
{
    public class ColorChanger
    {
        private readonly SpriteRenderer _render;

        private readonly Color _baseColor;

        public ColorChanger(SpriteRenderer render)
        {
            _render = render;
            _baseColor = _render.color;
        }

        public void Change(Color color)
        {
            _render.DOColor(color, 0.15f);
        }

        public void Reset()
        {
            _render.color = _baseColor;
        }
    }
}