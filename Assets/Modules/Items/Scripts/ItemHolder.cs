using UnityEngine;

namespace Potions.Gameplay
{
    [RequireComponent(typeof(Sprite))]
    public class ItemHolder : MonoBehaviour
    {
        public ItemData Item => ItemDatabase.GetItem(_id);
        public string ItemId => _id;

        public void SetItem(string id, bool animate = true)
        {
            _id = id;
            _spriteRenderer.sprite = Item ? Item.Sprite : null;

            if (animate)
            {
                LeanTween.cancel(_spriteRenderer.gameObject);
                _spriteRenderer.transform.localScale = Vector3.one * 1.2f;
                LeanTween.scale(_spriteRenderer.gameObject, Vector3.one, 0.15f).setEaseOutCubic();
            }
        }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private string _id;
    }
}