using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Potions.Gameplay
{
    [RequireComponent(typeof(ItemHolder))]
    public class ItemGhost : MonoBehaviour
    {
        public void Setup(string id)
        {
            GetComponent<ItemHolder>().SetItem(id);
            var seq = LeanTween.sequence();

            // seq.append(0.25f);
            seq.append(LeanTween.moveLocalY(gameObject, transform.localPosition.y + 0.7f, 0.3f).setEaseOutCubic());
            seq.append(LeanTween.scale(gameObject, Vector3.one * 1.85f, 0.2f).setEaseInCubic());
            seq.insert(LeanTween.alpha(gameObject, 0, 0.15f));
            seq.append(() => Destroy(gameObject));
        }
    }
}
