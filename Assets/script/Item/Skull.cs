using UnityEngine;
namespace PPman
{
    /// <summary>
    ///骷髏道具類別 
    /// </summary>
    public class Skull : Item
    {
        protected override void GetItem()
        {
            base.GetItem();
            SoundManager.Instance.PlaySound(Soundtype.EatItem, 0.8f, 1.1f);
            NPC_knight.Instance.GetItem();
        }


    }
}
