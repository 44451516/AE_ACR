using AE_ACR_DRK_Setting;
using AE_ACR_DRK_Triggers;
using AE_ACR_DRK.SlotResolvers;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using AEAssist.Verify;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.IoC;
using Dalamud.Plugin.Services;
using ImGuiNET;

namespace AE_ACR_DRK;

// 重要 类一定要Public声明才会被查找到
public class DKRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "44451516";

    [PluginService] internal static IChatGui ChatGui { get; private set; } = null!;

    // 逻辑从上到下判断，通用队列是无论如何都会判断的 
    // gcd则在可以使用gcd时判断
    // offGcd则在不可以使用gcd 且没达到gcd内插入能力技上限时判断
    // pvp环境下 全都强制认为是通用队列
    private List<SlotResolverData> SlotResolvers = new()
    {
        // offGcd队列
        new(new DK_Ability_掠影示现(), SlotMode.OffGcd),
        new(new DK_Ability_暗黑波动_AOE(), SlotMode.OffGcd),
        new(new DK_Ability_暗黑锋(), SlotMode.OffGcd),
        new(new DK_Ability_嗜血(), SlotMode.OffGcd),
        new(new DK_Ability_吸血深渊(), SlotMode.OffGcd),
        new(new DK_Ability_精雕怒斩(), SlotMode.OffGcd),
        new(new DK_Ability_暗影使者(), SlotMode.OffGcd),
        new(new DK_Ability_腐秽大地(), SlotMode.OffGcd),

        // gcd队列
        new(new DK_GCD_蔑视厌恶(), SlotMode.Gcd),
        new(new DK_GCD_寂灭(), SlotMode.Gcd),
        new(new DK_GCD_血溅(), SlotMode.Gcd),
        new(new DK_GCD_AOE_Base(), SlotMode.Gcd),
        new(new DK_GCD_Base(), SlotMode.Gcd),
    };


    public Rotation Build(string settingFolder)
    {
#if DEBUG
        AELoggerUtils.init();
#endif


        // 初始化设置
        DKSettings.Build(settingFolder);
        // 初始化QT （依赖了设置的数据）
        BuildQT();

        var rot = new Rotation(SlotResolvers)
        {
            // TargetJob = Jobs.Marauder,
            TargetJob = Jobs.DarkKnight,
            AcrType = AcrType.Both,
            MinLevel = 30,
            MaxLevel = 100,
            Description = "DK先行版",
        };

        // 添加各种事件回调
        rot.SetRotationEventHandler(new DKRotationEventHandler());
        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new TriggerAction_QT());

        return rot;
    }

    // 声明当前要使用的UI的实例 示例里使用QT
    public static JobViewWindow QT { get; private set; }

    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }

    // 构造函数里初始化QT
    public void BuildQT()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        QT = new JobViewWindow(DKSettings.Instance.JobViewSave, DKSettings.Instance.Save, "AE DK [仅作为开发示范]");
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        QT.AddTab("Dev", DrawQtDev);
        QT.AddTab("通用", DrawQtGeneral);

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

    // 设置界面
    public void OnDrawSetting()
    {
        DKSettingUI.Instance.Draw();
    }

    public void OnUIUpdate()
    {
    }

    public void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        ImGui.Text($"GetRecastTime:{Core.Resolve<MemApiSpell>().GetRecastTime(DKData.疾跑).TotalSeconds}");
        ImGui.Text($"GetRecastTimeElapsed:{Core.Resolve<MemApiSpell>().GetRecastTimeElapsed(DKData.疾跑)}");
        ImGui.Text($"GetCooldownRemainingTime:{DKData.疾跑.GetCooldownRemainingTime()}");
        ImGui.Text($"GetCooldown:{Core.Resolve<MemApiSpell>().GetCooldown(DKData.疾跑).TotalSeconds}");


        ImGui.Text($"LastSpell : {Core.Resolve<MemApiSpellCastSuccess>().LastSpell}");
        ImGui.Text($"LastAbility :  {Core.Resolve<MemApiSpellCastSuccess>().LastAbility}");
        ImGui.Text($"LastGcd : {Core.Resolve<MemApiSpellCastSuccess>().LastGcd}");
        ImGui.Text($"GetLastComboSpellId : {Core.Resolve<MemApiSpell>().GetLastComboSpellId()}");

        ImGui.Text($"战斗时间1 : {CombatTime.Instance.combatStart}");
        ImGui.Text($"战斗时间2 : {CombatTime.Instance.combatEnd}");
        ImGui.Text($"战斗时间3 : {CombatTime.Instance.CombatEngageDuration().TotalSeconds}");
        ImGui.Text($"暗黑时间 : {Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining}");
        ImGui.Text($"暗黑时间 : {Core.Resolve<MemApiSpell>().CheckActionChange(DKData.暗黑锋).GetSpell().Id} - {Core.Resolve<MemApiSpell>().CheckActionChange(DKData.EdgeOfShadow).GetSpell().Id}");
        ImGui.Text($"LastSpell : {Core.Resolve<MemApiSpellCastSuccess>().LastSpell}");
        ImGui.Text($"刚魂StalwartSoul : {DKData.刚魂StalwartSoul.IsUnlock()}");
        ImGui.Text($"Scorn : {GameObjectExtension.HasAura(Core.Me, DKData.Buffs.Scorn, 0)}");
        ImGui.Text($"IsUnlock : {DKData.蔑视厌恶Disesteem.IsUnlock()}");
        ImGui.Text($"血乱Delirium : {DKData.血乱Delirium.GetCooldownRemainingTime()}");
        ImGui.Text($"血溅Bloodspiller : {DKData.血溅Bloodspiller.GetCooldownRemainingTime()}");
        ImGui.Text($"LivingShadow : {DKData.LivingShadow.GetCooldownRemainingTime()}");


        ImGui.Text($"自身中心数量 : {TargetHelper.GetNearbyEnemyCount(5)}");
        IBattleChara? battleChara = Core.Me.GetCurrTarget();
        ImGui.Text($"目标中心数量 : {TargetHelper.GetNearbyEnemyCount(battleChara, 5, 5)}");
        ImGui.Text($"血乱buff计时器 : {Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, DKData.Buffs.血乱Delirium, true)}");
    }

    public void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画Dev信息");
        foreach (var v in jobViewWindow.GetQtArray())
        {
            ImGui.Text($"Qt按钮: {v}");
        }

        foreach (var v in jobViewWindow.GetHotkeyArray())
        {
            ImGui.Text($"Hotkey按钮: {v}");
        }
    }


    public void Dispose()
    {
        // 释放需要释放的东西 没有就留空
    }
}