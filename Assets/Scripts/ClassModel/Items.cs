using Define;
using System;

/// <summary>
/// �����ڱ��������Item
/// </summary>
public class Item //���๹�캯���ǵø������define��ֵ,��Ϊû�еĻ�������дdefine����
{
    public int count { get; set; }
    public virtual IIconItemDefine Icondefine { get; set; }
    public Type Type { get => this.GetType(); }
}

//����
//public class SeedItem : Item
//{
//    public SeedDefine define { get; set; }
//    public SeedItem(SeedDefine define)
//    {
//        base.Icondefine = define;
//        this.define = define;
//        this.count = 0;
//    }
//}