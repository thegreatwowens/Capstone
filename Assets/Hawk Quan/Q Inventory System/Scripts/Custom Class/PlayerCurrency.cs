using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QInventory
{
    [System.Serializable]
    public class PlayerCurrency
    {
        public Currency currency;
        public float amount;
        public TextMeshProUGUI showText;

        public PlayerCurrency(Currency currency,float amount)
        {
            this.currency = currency;
            this.amount = amount;
        }
    }
}
