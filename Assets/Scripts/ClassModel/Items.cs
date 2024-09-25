using Define;
using System;

/// <summary>
/// �����ڱ��������Item
/// </summary>
public class Item //���๹�캯���ǵø������define��ֵ,��Ϊû�в�����дdefine����
{
    public int count { get; set; }
    public virtual IIconItemDefine Icondefine { get; set; }
    public Type Type { get => this.GetType(); }
}
public class SeedItem : Item
{
    public SeedDefine define { get; set; }
    public SeedItem(SeedDefine define)
    {
        base.Icondefine = define;
        this.define = define;
        this.count = 0;
    }
}

public class HarvestItem:Item
{
    public HarvestDefine define { get; set; }
    public HarvestItem(HarvestDefine define)
    {
        base.Icondefine = define;
        this.define = define;
        this.count = 0;
    }
}

public class FoodItem:Item
{
    public FoodDefine define { get; set; }
    public FoodItem(FoodDefine define)
    {
        base.Icondefine = define;
        this.define = define;
        this.count = 0;
    }
}

public class SpecialtyItem : Item
{
    public SpecialtyDefine define { get; set; }
    public SpecialtyItem(SpecialtyDefine define)
    {
        base.Icondefine = define;
        this.define = define;
        this.count = 0;
    }
}
