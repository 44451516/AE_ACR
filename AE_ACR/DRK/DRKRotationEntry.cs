#region

using AE_ACR_DRK_Setting;
using AE_ACR_DRK_Triggers;
using AE_ACR_DRK.SlotResolvers;
using AE_ACR.Base;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.DRK.SlotResolvers.减伤;
using AE_ACR.DRK.起手;
using AE_ACR.PLD.起手;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Extension;
using ImGuiNET;

#endregion

namespace AE_ACR_DRK;

// 重要 类一定要Public声明才会被查找到
public class DRKRotationEntry : IRotationEntry
{
    // 逻辑从上到下判断，通用队列是无论如何都会判断的 
    // gcd则在可以使用gcd时判断
    // offGcd则在不可以使用gcd 且没达到gcd内插入能力技上限时判断
    // pvp环境下 全都强制认为是通用队列
    private readonly List<SlotResolverData> SlotResolvers = new()
    {
        // offGcd队列
        new SlotResolverData(new Ability_深恶痛绝(), SlotMode.Always),
        new SlotResolverData(new Ability_行尸走肉(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_暗影墙(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_铁壁(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_弃明投暗(), SlotMode.OffGcd),

        new SlotResolverData(new Ability_黑盾(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_献奉(), SlotMode.OffGcd),

        new SlotResolverData(new Ability_亲疏自行(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_雪仇(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_暗黑布道(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_挑衅(), SlotMode.OffGcd),

        new SlotResolverData(new DKUsePotion(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_暗影使者(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_腐秽大地(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_腐秽黑暗(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_掠影示现(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_暗黑波动_AOE(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_暗黑锋(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_嗜血(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_吸血深渊(), SlotMode.OffGcd),
        new SlotResolverData(new DK_Ability_精雕怒斩(), SlotMode.OffGcd),

        // gcd队列
        new SlotResolverData(new DK_GCD_伤残(), SlotMode.Gcd),
        new SlotResolverData(new DK_GCD_蔑视厌恶(), SlotMode.Gcd),
        new SlotResolverData(new DK_GCD_寂灭(), SlotMode.Gcd),
        new SlotResolverData(new DK_GCD_血溅(), SlotMode.Gcd),
        new SlotResolverData(new DK_GCD_AOE_Base(), SlotMode.Gcd),
        new SlotResolverData(new DK_GCD_Base(), SlotMode.Gcd)
    };

    public static JobViewWindow QT { get; private set; }
    public string AuthorName { get; set; } = "44451516";

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
            Description = "开启QT起手序列和[通用]里面的选项冲突\n"
                          + "[赞资源]开启嗜血血乱、掠影示现、暗影使者、暗黑锋、精雕怒斩、腐秽大地、蔑视厌恶、血溅将不会释放\n"
                          + "如果你发现没有保留蓝量，请把【目标小于多少血打完所有资源】设置为0"
        };

        // 添加各种事件回调
        rot.SetRotationEventHandler(new DRKRotationEventHandler());
        // 添加QT开关的时间轴行为
        rot.AddTriggerAction(new TriggerAction_QT());
        rot.AddOpener(GetOpener);

        return rot;
    }

    private IOpener? GetOpener(uint level)
    {
        if (level == 100)
        {
            return new DRK_Opener100();
        }

        if (level >= 80 && level < 96)
        {
            return new DRK_Opener90();
        }


        return null;
    }

    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }

    // 设置界面
    public void OnDrawSetting()
    {
        DKSettingUI.Instance.Draw();
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
        QT = new JobViewWindow(DKSettings.Instance.JobViewSave, DKSettings.Instance.Save, "DK");
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容


#if DEBUG
        QT.AddTab("Dev", DrawQtDev);
#endif
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("说明", DrawQtDescription);
        QT.AddTab("日常模式", DrawDailyMode);

        QT.AddQt(BaseQTKey.停手, false, "是否使用基础的Gcd");
        QT.AddQt(BaseQTKey.攒资源, false, "攒资源不会卸暗血");
        QT.AddQt(BaseQTKey.爆发药, true);
        QT.AddQt(DRKQTKey.腐秽大地, true);
        QT.AddQt(DRKQTKey.腐秽黑暗, true);
        QT.AddQt(DRKQTKey.暗影使者, true);
        QT.AddQt(DRKQTKey.蔑视厌恶, true);
        QT.AddQt(DRKQTKey.掠影示现, true);
        QT.AddQt(DRKQTKey.精雕吸血, true);
        QT.AddQt(DRKQTKey.伤残, false, "和目标距离过远的时候使用");
        QT.AddQt(DRKQTKey.起手序列, true);

        QT.AddHotkey("LB", new HotKeyResolver_LB());
        
    }

    private void DrawDailyMode(JobViewWindow obj)
    {
        var DkSettings = DKSettings.Instance;
        {

            ImGui.Text("日常模式会持续开盾，和自动减伤");
            ImGui.SetNextItemWidth(150f);
            ImGui.Checkbox("启用", ref DkSettings.日常模式);
            ImGui.SetNextItemWidth(150f);
            ImGui.Checkbox("使用挑衅", ref DkSettings.挑衅);
            ImGui.SetNextItemWidth(150f);
            ImGui.Checkbox("日常模式_残血不打爆发[测试中]", ref DkSettings.日常模式_残血不打爆发);
            ImGui.Spacing();
        }
    }

    private void DrawQtDescription(JobViewWindow obj)
    {
        ImGui.Text("如果你发现他打小怪没有保留蓝量，请把【目标小于多少血打完所有资源】设置为0");
    }



    public void OnUIUpdate()
    {
    }

    public void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        DKSettingUI.BaseDarw();
    }

    public void DrawQtDev(JobViewWindow jobViewWindow)
    {
        // ImGui.Text("画Dev信息");
        // foreach (var v in jobViewWindow.GetQtArray())
        // {
        //     ImGui.Text($"Qt按钮: {v}");
        // }
        //
        //
        // foreach (var v in jobViewWindow.GetHotkeyArray())
        // {
        //     ImGui.Text($"Hotkey按钮: {v}");
        // }


        var battleChara = Core.Me.GetCurrTarget();
        ImGui.Text($"血溅Bloodspiller.IsUnlock:{DRKBaseSlotResolvers.血溅Bloodspiller.MyIsUnlock()}");
        ImGui.Text($"强化药:{RaidBuff.强化药.GetBuffRemainingTime() / 1000f}");
        // ImGui.Text($"血溅Bloodspiller.IsUnlock:{Core.Resolve<MemApiSpell>().CheckActionChange(DRKBaseSlotResolvers.血溅Bloodspiller).GetSpell()}");
        // ImGui.Text($"目标距离:{TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me)}");
        // ImGui.Text($"挑衅cd:{DRKBaseSlotResolvers.挑衅.GetCooldownRemainingTime()}");
        // ImGui.Text($"GetRecastTime:{Core.Resolve<MemApiSpell>().GetRecastTime(DRKBaseSlotResolvers.疾跑).TotalSeconds}");
        // ImGui.Text($"GetRecastTimeElapsed:{Core.Resolve<MemApiSpell>().GetRecastTimeElapsed(DRKBaseSlotResolvers.疾跑)}");
        // ImGui.Text($"GetCooldownRemainingTime:{DRKBaseSlotResolvers.Shadowbringer暗影使者.GetCooldownRemainingTime()}");
        // ImGui.Text($"ActionReady:{DRKBaseSlotResolvers.Shadowbringer暗影使者.ActionReady()}");
        //
        //
        // ImGui.Text($"LastSpell : {Core.Resolve<MemApiSpellCastSuccess>().LastSpell}");
        // ImGui.Text($"LastAbility :  {Core.Resolve<MemApiSpellCastSuccess>().LastAbility}");
        // ImGui.Text($"LastGcd : {Core.Resolve<MemApiSpellCastSuccess>().LastGcd}");
        // ImGui.Text($"GetLastComboSpellId : {Core.Resolve<MemApiSpell>().GetLastComboSpellId()}");
        //
        // ImGui.Text($"战斗时间1 : {CombatTime.Instance.combatStart}");
        // ImGui.Text($"战斗时间2 : {CombatTime.Instance.combatEnd}");
        // ImGui.Text($"战斗时间3 : {CombatTime.Instance.CombatEngageDuration().TotalSeconds}");
        // ImGui.Text($"暗黑时间 : {Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining}");
        // ImGui.Text($"暗黑时间 : {Core.Resolve<MemApiSpell>().CheckActionChange(DRKBaseSlotResolvers.暗黑锋).GetSpell().Id} - {Core.Resolve<MemApiSpell>().CheckActionChange(DRKBaseSlotResolvers.EdgeOfShadow).GetSpell().Id}");
        // ImGui.Text($"LastSpell : {Core.Resolve<MemApiSpellCastSuccess>().LastSpell}");
        // ImGui.Text($"刚魂StalwartSoul : {DRKBaseSlotResolvers.刚魂StalwartSoul.MyIsUnlock()}");
        // ImGui.Text($"Scorn : {Core.Me.HasAura(DRKBaseSlotResolvers.Buffs.Scorn)}");
        // ImGui.Text($"IsUnlock : {DRKBaseSlotResolvers.蔑视厌恶Disesteem.MyIsUnlock()}");
        // ImGui.Text($"血乱Delirium : {DRKBaseSlotResolvers.血乱Delirium.GetCooldownRemainingTime()}");
        // ImGui.Text($"血溅Bloodspiller : {DRKBaseSlotResolvers.血溅Bloodspiller.GetCooldownRemainingTime()}");
        // ImGui.Text($"LivingShadow : {DRKBaseSlotResolvers.LivingShadow.GetCooldownRemainingTime()}");
        //
        //
        // ImGui.Text($"自身中心数量 : {TargetHelper.GetNearbyEnemyCount(5)}");
        // ImGui.Text($"目标中心数量 : {TargetHelper.GetNearbyEnemyCount(battleChara, 5, 5)}");
        // ImGui.Text($"血乱buff计时器 : {Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, DRKBaseSlotResolvers.Buffs.血乱Delirium, true)}");
        // ImGui.Text($"腐秽大地SaltedEarth : {DRKBaseSlotResolvers.腐秽大地SaltedEarth.ActionReady()}");
        // ImGui.Text($"腐秽黑暗 : {DRKBaseSlotResolvers.腐秽黑暗.ActionReady()}");
        // ImGui.Text($"OriginalHook : {DRKBaseSlotResolvers.腐秽大地SaltedEarth.OriginalHook()}");
    }
}