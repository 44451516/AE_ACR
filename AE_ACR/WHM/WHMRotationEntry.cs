#region

using AE_ACR.WAR;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;

#endregion

namespace AE_ACR.WHM;

#if DEBUG
public class WHMRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "44451516";
    
    public Rotation Build(string settingFolder)
    {
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.WhiteMage,
            AcrType = AcrType.Both,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "这个ACR没什么用，留空的"
        };

        return rot;
    }

    public IRotationUI GetRotationUI()
    {
        return new JobViewWindow(new JobViewSave(), () => { },"");
    }

    
    private readonly List<SlotResolverData> SlotResolvers = new()
    {


    };
    
    public void OnDrawSetting()
    {

    }


    public void Dispose()
    {

    }
}
#endif