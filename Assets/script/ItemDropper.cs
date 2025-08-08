using UnityEngine;
namespace PPman
{
    public class ItemDropper : MonoBehaviour
    {
        [Header("掉落設定")]
        [SerializeField, Range(0, 1)] private float dropRate = 1f;
        [SerializeField] private GameObject[] dropItems;

        public void TryDrop()
        {
            if (dropItems.Length == 0) return;

            if (Random.value <= dropRate)
            {
                GameObject drop = dropItems[Random.Range(0, dropItems.Length)];
                GameObject temp = Instantiate(drop, transform.position + Vector3.up * 0.5f, Quaternion.identity);

                Rigidbody2D rb = temp.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = new Vector2(Random.Range(-1.5f, 1.5f), 0.1f);
                    SoundManager.Instance.PlaySound(Soundtype.DropItem, 0.8f, 1.5f);
                }
            }
        }
    }
}