using System;

namespace Messages
{
    public class ItemAlert : EventArgs
    {
        public ItemAlert(string itemInfo)
        {
            ItemInfo = itemInfo;
        }

        public string ItemInfo { get; }
    }
}