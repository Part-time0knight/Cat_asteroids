using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Domain.Dto
{
    public class ShopMiniDto
    {
        public List<BundleDtoMini> Bundles = new(3);
    }

    public class BundleDtoMini
    {
        public Sprite TopIcon;
        public bool TopActive;
        public bool BottomActive;
        public Sprite BottomIcon;
        public Action OnClick;
    }
}