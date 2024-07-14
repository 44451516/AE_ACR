using AE_ACR.PLD.Setting;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.PLD.Triggers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.JobApi;
using ImGuiNET;
using Ability_铁壁 = AE_ACR.PLD.SlotResolvers.Ability_铁壁;

namespace AE_ACR.PLD;

// 重要 类一定要Public声明才会被查找到
public class PLDRotationEntry : IRotationEntry
{
    // 逻辑从上到下判断，通用队列是无论如何都会判断的 
    // gcd则在可以使用gcd时判断
    // offGcd则在不可以使用gcd 且没达到gcd内插入能力技上限时判断
    // pvp环境下 全都强制认为是通用队列
    private readonly List<SlotResolverData> SlotResolvers = new()
    {
        new SlotResolverData(new Ability_神圣领域(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_预警(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_铁壁(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_壁垒(), SlotMode.OffGcd),

        new SlotResolverData(new Ability_圣盾阵(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_亲疏自行(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_雪仇(), SlotMode.OffGcd),


        new SlotResolverData(new Ability_战逃反应(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_安魂祈祷(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_深奥之灵(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_厄运流转(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_深奥之灵(), SlotMode.OffGcd),

        // new(new Ability_铁壁(), SlotMod
        // .
        // e.OffGcd),
        // new(new Ability_预警(), SlotMode.OffGcd),


        // gcd队列
        new SlotResolverData(new GCD_沥血剑(), SlotMode.Gcd),
        new SlotResolverData(new GCD_大宝剑连击(), SlotMode.Gcd),
        new SlotResolverData(new GCD_Base(), SlotMode.Gcd)
    };

    // 声明当前要使用的UI的实例 示例里使用QT
    public static JobViewWindow QT { get; private set; }
    public string AuthorName { get; set; } = "44451516";


    public Rotation Build(string settingFolder)
    {
#if DEBUG
        AELoggerUtils.init();
#endif


        // 初始化设置
        Settings.Build(settingFolder);
        // 初始化QT （依赖了设置的数据）
        BuildQT();

        var rot = new Rotation(SlotResolvers)
        {
            // TargetJob = Jobs.Marauder,
            // TargetJob = Jobs.Gladiator,
            TargetJob = Jobs.Paladin,
            AcrType = AcrType.Normal,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "骑士"
        };

        // 添加各种事件回调
        rot.SetRotationEventHandler(new RotationEventHandler());
        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new TriggerAction_QT());

        return rot;
    }

    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }

    // 设置界面
    public void OnDrawSetting()
    {
        SettingUI.Instance.Draw();
    }


    public void Dispose()
    {
        // 释放需要释放的东西 没有就留空
    }

    // 构造函数里初始化QT
    public void BuildQT()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        QT = new JobViewWindow(Settings.Instance.JobViewSave, Settings.Instance.Save, "PLD");
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        // QT.AddTab("Dev", DrawQtDev);
        QT.AddTab("通用", DrawQtGeneral);

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        QT.AddQt(BaseQTKey.停手, false, "是否使用基础的Gcd");
        QT.AddQt(BaseQTKey.减伤, true);
        // QT.AddQt(QTKey.Test2, false);
        // QT.AddQt(QTKey.UsePotion,false);

        // 添加快捷按钮 (带技能图标)
        // QT.AddHotkey("战斗之声",
        //     new HotKeyResolver_NormalSpell(SpellsDefine.LegSweep, SpellTargetType.Self));
        // QT.AddHotkey("失血",
        //     new HotKeyResolver_NormalSpell(SpellsDefine.LegSweep, SpellTargetType.Target));

        // QT.AddHotkey("爆发药1", new HotKeyResolver_Potion());
        // QT.AddHotkey("爆发药1", new HotKeyResolver_Potion());
        // QT.AddHotkey("极限技2", new HotKeyResolver_LB());

        /*
        // 这是一个自定义的快捷按钮 一般用不到
        // 图片路径是相对路径 基于AEAssist(C|E)NVersion/AEAssist
        // 如果想用AE自带的图片资源 路径示例: Resources/AE2Logo.png
        QT.AddHotkey("极限技", new HotkeyResolver_General("#自定义图片路径", () =>
        {
            // 点击这个图片会触发什么行为
            LogHelper.Print("你好");
        }));
        */
    }

    public void OnUIUpdate()
    {
    }

    public void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        // {
        //     GCHandle handle = GCHandle.Alloc(QT);
        //
        //     var pin = GCHandle.ToIntPtr(handle);
        //
        //     ImGui.Text($"赎罪剑Atonement3 : {pin}");
        // }
        // {
        //     GCHandle handle = GCHandle.Alloc(GLDRotationEntry.QT);
        //
        //     var pin = GCHandle.ToIntPtr(handle);
        //
        //     ImGui.Text($"赎罪剑Atonement3 : {pin}");
        // }

        var Oath = Core.Resolve<JobApi_Paladin>().Oath;

        ImGui.Text($"大保健连击Confiteor : {PLDBaseSlotResolvers.大保健连击Confiteor.OriginalHook().Id}");
        ImGui.Text($"赎罪剑Atonement1 : {PLDBaseSlotResolvers.赎罪剑Atonement1.OriginalHook().Id}");
        ImGui.Text($"GCD : {GCDHelper.GetGCDCooldown()}");
        ImGui.Text($"能量值 : {Oath}");
        ImGui.Text($"圣灵buff : {BaseIslotResolver.GetBuffRemainingTime(PLDBaseSlotResolvers.Buffs.DivineMight)}");
    }

    public void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画Dev信息");
        foreach (var v in jobViewWindow.GetQtArray()) ImGui.Text($"Qt按钮: {v}");

        foreach (var v in jobViewWindow.GetHotkeyArray()) ImGui.Text($"Hotkey按钮: {v}");
    }
}