using System.Collections.Generic;
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


        [Inject]
        private void Construct(Pool pool)
        {
            _pool = pool;
            var item = _pool.Spawn();
            _container = item.transform.parent;
            _pool.Despawn(item);
        }

        private void Awake()
        {
            
        }

        public void SetPanelActive(bool active)
        {
            _container.gameObject.SetActive(active);
        }

        public void SetHits(int hits)
        {
            while (_hits.Count > 0)
            {
                var hit = _hits[0];
                _pool.Despawn(hit);
                _hits.RemoveAt(0);
            }

            int i = 0;
            while (i++ < hits)
                _hits.Add(_pool.Spawn());
        }

        public class Pool : MonoMemoryPool<Image>
        {
        }
    }
}