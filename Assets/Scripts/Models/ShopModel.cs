using QFramework;
using System.Collections.Generic;
using Define;
using SaveData;

namespace Models
{
    public class ShopModel : AbstractModel
    {
        public Dictionary<int, Dictionary<int, ShopItem>> shopItemDict = new Dictionary<int, Dictionary<int, ShopItem>>();//��һ��key���̵�id���ڶ���key��shopItem��id
        public List<ShopItem> shopItemList = new List<ShopItem>();
        public Dictionary<int, ShopDefine> shopDefines = new Dictionary<int, ShopDefine>();
        protected override void OnInit()
        {
            //���̵���Ʒ����Ϊ��ʼ����
            var defines= this.SendQuery(new GetDefineDictionaryQuery<Dictionary<int, Dictionary<int, ShopItemDefine>>>());
            foreach(var s in defines)
            {
                shopItemDict[s.Key] = new Dictionary<int, ShopItem>();
                foreach(var si in s.Value)
                {
                    IIconItemDefine itemDefine = this.SendQuery(new GetIconItemDefineQuery(si.Value.ItemId));
                    ShopItem item = new ShopItem(si.Value, itemDefine, si.Value.sellCount);
                    shopItemDict[s.Key][si.Key] = item;
                    shopItemList.Add(item);
                }
            }

            Load();
            CommonMono.AddQuitAction(Save);
        }

        void Load()
        {
            ShopSaveData shopData = this.GetUtility<Storage>().Load<ShopSaveData>();
            if (shopData == default)
                return;
            shopDefines = this.SendQuery(new GetDefineDictionaryQuery<Dictionary<int, ShopDefine>>());
            foreach (var shop in shopItemDict)
            {
                if (this.SendQuery(new JudgeExitTimeOneDayApartClockQuery(shopDefines[shop.Key].RefreshTime)))//ˢ������
                {
                    foreach (var item in shop.Value)
                    {
                        item.Value.count = item.Value.define.sellCount;
                        item.Value.status = shopData.shopItemDict[shop.Key][item.Key].status;
                    }
                }
                else//��ˢ������
                {
                    foreach (var item in shop.Value)
                    {
                        item.Value.count = shopData.shopItemDict[shop.Key][item.Key].count;
                        item.Value.status = shopData.shopItemDict[shop.Key][item.Key].status;
                    }
                }
            }
        }

        void Save()
        {
            ShopSaveData shopData = new ShopSaveData();
            shopData.shopItemDict = new Dictionary<int, Dictionary<int, ShopItemSaveData>>();
            shopData.shopItemList = new List<ShopItemSaveData>();
            foreach (var shop in shopItemDict)
            {
                shopData.shopItemDict[shop.Key] = new Dictionary<int, ShopItemSaveData>();
                foreach (var item in shop.Value)
                {
                    ShopItemSaveData data = new ShopItemSaveData();
                    data.count = item.Value.count;
                    data.status = item.Value.status;
                    shopData.shopItemDict[shop.Key][item.Key] = data;
                    shopData.shopItemList.Add(data);
                }
            }
            this.GetUtility<Storage>().Save<ShopSaveData>(shopData);
        }
    }
}