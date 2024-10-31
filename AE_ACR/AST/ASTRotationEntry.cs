#region

using AE_ACR.AST.Setting;
using AE_ACR.AST.SlotResolvers;
using AE_ACR.AST.Triggers;
using AE_ACR.Base;
using AE_ACR.GLA.Setting;
using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using ImGuiNET;
using GCD_Base = AE_ACR.AST.SlotResolvers.GCD_Base;

#endregion

namespace AE_ACR.AST;

public class ASTRotationEntry : IRotationEntry
{
    private readonly List<SlotResolverData> SlotResolvers = new()
    {
        new SlotResolverData(new GCD_生辰(), SlotMode.Always),
        new SlotResolverData(new Ability_醒梦(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_命运之轮(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_大宇宙(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_天宫图(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_天星冲日(), SlotMode.OffGcd),


        new SlotResolverData(new Ability_先天禀赋(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_天星交错(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_擢升(), SlotMode.OffGcd),

        new SlotResolverData(new Ability_光速(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_占卜(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_地星(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_出卡1(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_出卡4(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_出卡3(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_抽卡(), SlotMode.OffGcd),

        new SlotResolverData(new GCD_奶自己(), SlotMode.Gcd),

        new SlotResolverData(new Ability_中间学派(), SlotMode.OffGcd),
        new SlotResolverData(new GCD_群奶治疗(), SlotMode.Gcd),
        new SlotResolverData(new GCD_奶T(), SlotMode.Gcd),
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
        ASTSettings.Build(settingFolder);
        // 初始化QT （依赖了设置的数据）
        BuildQT();

        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Astrologian,
            AcrType = AcrType.Normal,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "这是占星刷亲信的ACR，没有任何QT和HOKEY\n "
        };

        // 添加各种事件回调
        rot.SetRotationEventHandler(new ASTRotationEventHandler());
        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new AST_TriggerAction_QT());

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
        QT = new JobViewWindow(Settings.Instance.JobViewSave, Settings.Instance.Save, "AST");
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        QT.AddTab("Dev", DrawQtDev);
        QT.AddTab("反馈建议", UIHelp.Feedback);
        // QT.AddTab("通用", DrawQtGeneral);

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        // QT.AddQt(QTKey.UseBaseGcd, true, "是否使用基础的Gcd");
        // QT.AddQt(QTKey.Test1, true);
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
    }

    public void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text($"落陷凶星1 : {ASTBaseSlotResolvers.落陷凶星.OriginalHook().Id}");
        ImGui.Text($"dot1 : {ASTBaseSlotResolvers.Play1.OriginalHook().Id}");
        ImGui.Text($"GetBuffRemainingTime : {BaseIslotResolver.GetBuffRemainingTime(BaseIslotResolver.DeBuffs.加速器炸弹)}");
        ImGui.Text($"HasEffect : {BaseIslotResolver.HasEffect(BaseIslotResolver.DeBuffs.加速器炸弹)}");
        ImGui.Text($"地星ActionReady : {ASTBaseSlotResolvers.地星.ActionReady()}");
        ImGui.Text($"地星IsReady : {ASTBaseSlotResolvers.地星.IsReady()}");
        ImGui.Text($"地星IsUnlock : {ASTBaseSlotResolvers.地星.MyIsUnlock()}");
        ImGui.Text($"地星IsLevelEnough : {ASTBaseSlotResolvers.地星.IsLevelEnough()}");
        ImGui.Text($"GetCooldownRemainingTime : {ASTBaseSlotResolvers.地星.GetCooldownRemainingTime()}");
        ImGui.Text($"Play1 :{ASTBaseSlotResolvers.Play1}-{ASTBaseSlotResolvers.Play1.OriginalHook().Id}");
        ImGui.Text($"Play2 :{ASTBaseSlotResolvers.Play2}- {ASTBaseSlotResolvers.Play2.OriginalHook().Id}");
        ImGui.Text($"Play3 :{ASTBaseSlotResolvers.Play3}-{ASTBaseSlotResolvers.Play3.OriginalHook().Id}");
        ImGui.Text($"Play4 :{ASTBaseSlotResolvers.Play4}-{ASTBaseSlotResolvers.Play4.OriginalHook().Id}");

        // var 目标 = PartyHelper.CastableAlliesWithin30.FirstOrDefault(r => r.CurrentHp == 0 || r.IsDead);
        //
        // if (目标 != null && 目标.IsValid())
        // {
        //     ImGui.Text($"HasEffect : {目标.Name}");
        // }


        // ImGui.Text($"AOE1 : {ASTBaseSlotResolvers.getTankHpOrderByPercent}");
        // ImGui.Text($"AOE1 : {ASTBaseSlotResolvers.getTankHpOrderByPercent().Name}");
    }
}