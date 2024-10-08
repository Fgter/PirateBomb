using Define;
using System;

/// <summary>
/// 可以在背包里面的Item
/// </summary>
public class Item //子类构造函数记得给父类的define赋值,因为没有的话不能重写define属性
{
    public int count { get; set; }
    public virtual IIconItemDefine Icondefine { get; set; }
    public Type Type { get => this.GetType(); }
}

//例子
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