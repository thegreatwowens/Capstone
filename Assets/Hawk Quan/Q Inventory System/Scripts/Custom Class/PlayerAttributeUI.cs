using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QInventory
{
    [System.Serializable]
    class PlayerAttributeUI
    {
        public ItemAttribute m_Attribute = null;
        public ShowType showType = ShowType.None;
        public TextMeshProUGUI showText = null;
        public Slider showSlider = null;
    }
}
