#region

#endregion

namespace AE_ACR.WAR;

#if DEBUG
// 重要 类一定要Public声明才会被查找到
public class WARRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "44451516";

    private readonly List<SlotResolverData> SlotResolvers = new()
    {
   
        
    };

    public static JobViewWindow QT { get; private set; }

    public Rotation Build(string settingFolder)
    {
        WARSettings.Build(settingFolder);
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Warrior,
            AcrType = AcrType.Both,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "这个ACR没什么用，留空的"
        };
        
        return rot;
    }

    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        QT = new JobViewWindow(WARSettings.Instance.JobViewSave, WARSettings.Instance.Save, "WAR");
        return QT;
    }

    // 设置界面
    public void OnDrawSetting()
    {
      
    }


    public void Dispose()
    {
      
    }
    
}
#endif