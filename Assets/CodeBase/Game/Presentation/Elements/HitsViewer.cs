using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.Elements
{
    public class HitsViewer : MonoBehaviour
    {
        private readonly List<Image> _hits = new();
        private Pool _pool;
        private Transform _container;

        [Inject(Id = "Heart")] private Sprite _heartSprite;
        [Inject(Id = "Shield")] private Sprite _shieldSprite;

        [Inject]
        private void Construct(Pool pool)
        {
            _pool = pool;
            var item = _pool.Spawn();
            _container = item.transform.parent;
            _pool.Despawn(item);
        }

        public void SetPanelActive(bool active)
        {
            _container.gameObject.SetActive(active);
        }

        public void SetHits(int hits, int shieldHits)
        {
            Image hit;

            while (_hits.Count > 0)
            {
                hit = _hits[0];
                _pool.Despawn(hit);
                _hits.RemoveAt(0);
            }

            int i = 0, iShield;
            while (i++ < hits)
            {
                hit = _pool.Spawn();
                _hits.Add(hit);
                hit.sprite = _heartSprite;
                hit.transform.SetSiblingIndex(i);
            }
            iShield = i;
            i = 0;

            while (i++ < shieldHits)
            {
                hit = _pool.Spawn();
                _hits.Add(hit);
                hit.sprite = _shieldSprite;
                hit.transform.SetSiblingIndex(iShield++);
            }

            LayoutRebuilder.
                ForceRebuildLayoutImmediate(_container.GetComponent<RectTransform>());
        }

        public class Pool : MonoMemoryPool<Image>
        {
        }
    }
}