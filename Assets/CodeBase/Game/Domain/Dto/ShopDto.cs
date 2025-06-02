using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Domain.Dto
{
    public class ShopDto
    {
        public List<BundleDto> Bundles = new(3);
    }

    public class BundleDto
    {
        public string TopName;
        public string TopDescription;
        public Sprite TopIcon;

        public string BottomName;
        public string BottomDescription;
        public Sprite BottomIcon;

        public string Cost;
        public Action OnBuy;
    }
}