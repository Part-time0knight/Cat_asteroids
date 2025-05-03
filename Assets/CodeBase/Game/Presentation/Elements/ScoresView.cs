using Game.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoresView : MonoBehaviour
{
    [SerializeField] private Settings _settings;

    private Pool _pool;

    [Inject]
    private void Construct(Pool pool)
    {
        _pool = pool;
    }

    public void Show(List<GameplayViewModel.ScoreData> scores)
    {
        foreach(var score in scores)
        {
            var animation = _pool.Spawn();
            animation.Show(score.Score, score.Position, 
                _settings.ScoreAnimation.Duration,
                _settings.ScoreAnimation.EndPositionY);
            animation.OnEnd += Remove;
        }
    }

    private void Remove(ScoreAnimation animation)
    {
        animation.OnEnd -= Remove;
        _pool.Despawn(animation);
    }

    public class Pool : MonoMemoryPool<ScoreAnimation>
    {
        protected RectTransform _buffer;

        protected override void OnCreated(ScoreAnimation item)
        {
            item.transform.SetParent(_buffer);
            base.OnCreated(item);
        }

        [Inject]
        private void Construct(RectTransform container) 
        {
            _buffer = container;
        }
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public RectTransform Container { get; private set; }
        [field: SerializeField] public ScoreAnimationSettings ScoreAnimation { get; private set; }


        [Serializable]
        public class ScoreAnimationSettings
        {
            [field: SerializeField] public float Duration { get; private set; }
            [field: SerializeField] public float EndPositionY { get; private set; }
        }
    }
}
