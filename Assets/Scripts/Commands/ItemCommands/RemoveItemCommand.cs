using Models;
using QFramework;
using UnityEngine;

public class RemoveItemCommand : AbstractCommand<bool>
{
    int id;
    int count;
    public RemoveItemCommand(int id,int count)
    {
        this.id = id;
        this.count = count;
    }
    protected override bool OnExecute()
    {
        ItemModel model = this.GetModel<ItemModel>();
        if (model.Items.TryGetValue(id, out Item item))
        {
            if (item.count >= count)
            {
                item.count -= count;
                //�����ж�һ�µ���0��ʱ������ֵ��ж�Ӧ��ӳ���ϵ
                this.SendEvent(new ItemCountChangeEvent());
                return true;
            }
            return false;
        }
        else
        {
            Debug.LogError("id:" + id + " not in ItemModel");
            return false;
        }
        
    }
}
