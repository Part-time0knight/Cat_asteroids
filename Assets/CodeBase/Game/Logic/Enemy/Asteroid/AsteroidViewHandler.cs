using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidViewHandler
{
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Settings _settings;


    public AsteroidViewHandler(SpriteRenderer spriteRenderer, Settings settings)
    {
        _spriteRenderer = spriteRenderer;
        _settings = settings;
    }

    public void Initialize()
    {
        _spriteRenderer.sprite = _settings.Sprites[Random.Range(0, _settings.Sprites.Count)];
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public List<Sprite> Sprites { get; private set; }
    }
}
