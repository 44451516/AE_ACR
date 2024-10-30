#region

using AE_ACR.Base;
using AE_ACR.PLD.HotKey;
using AE_ACR.PLD.Setting;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.PLD.SlotResolvers.减伤;
using AE_ACR.PLD.Triggers;
using AE_ACR.PLD.起手;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.CombatRoutine.View.JobView.HotkeyResolver;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;
using ImGuiNET;

#endregion

namespace AE_ACR.PLD;

public class PLDRotationEntry : IRotationEntry
{
    private readonly List<SlotResolverData> SlotResolvers = new()
    {
        new SlotResolverData(new Ability_钢铁信念(), SlotMode.Always),
        new SlotResolverData(new PLDUsePotion(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_神圣领域(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_预警(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_铁壁(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_壁垒(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_圣光幕帘(), SlotMode.OffGcd),

        new SlotResolverData(new Ability_圣盾阵(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_亲疏自行(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_雪仇(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_挑衅(), SlotMode.OffGcd),


        new SlotResolverData(new Ability_战逃反应(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_安魂祈祷(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_深奥之灵(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_厄运流转(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_深奥之灵(), SlotMode.OffGcd),
        new SlotResolverData(new Ability_调停(), SlotMode.OffGcd),


        // gcd队列
        new SlotResolverData(new GCD_优先赎罪(), SlotMode.Gcd),
        new SlotResolverData(new GCD_优先强化圣灵(), SlotMode.Gcd),
        new SlotResolverData(new GCD_优先圣灵(), SlotMode.Gcd),
        new SlotResolverData(new GCD_沥血剑(), SlotMode.Gcd),
        new SlotResolverData(new GCD_大宝剑连击(), SlotMode.Gcd),
        new SlotResolverData(new GCD_倾泻资源(), SlotMode.Gcd),
        new SlotResolverData(new PLD_GCD_远程圣灵(), SlotMode.Gcd),
        new SlotResolverData(new PLD_GCD_投盾(), SlotMode.Gcd),
        new SlotResolverData(new PLD_GCD_Base_AOE(), SlotMode.Gcd),
        new SlotResolverData(new PLD_GCD_Base(), SlotMode.Gcd)


    };

    public static JobViewWindow QT { get; private set; }
    public string AuthorName { get; set; } = "44451516";

    public Rotation Build(string settingFolder)
    {
#if DEBUG
        AELoggerUtils.init();
#endif
        PLDSettings.Build(settingFolder);
        BuildQT();

        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Paladin,
            AcrType = AcrType.Both,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[战逃安魂]控制着所有的爆发\n"
                          + "[优先圣灵]满足圣灵的释放条件会一直用圣灵\n"
                          + "[优先赎罪]满足赎罪的释放条件会一直用赎罪\n"
                          + "[远程圣灵]拥有强化圣灵or达到设置阈值且不移动的时候使用\n"
                          + "[即刻战逃]会立刻使用战逃，即使没有合适的资源\n"
                          + "[大保健连击]会再未学习大保健的情况下用代替\n"
                          + "[一键减伤]铁壁-圣盾阵-预警-壁垒-神圣领域\n"
        };
        rot.AddOpener(GetOpener);
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
        PLDSettingUI.Instance.Draw();
    }


    public void Dispose()
    {

    }


    public void BuildQT()
    {
        QT = new JobViewWindow(PLDSettings.Instance.JobViewSave, PLDSettings.Instance.Save, "PLD");
        QT.SetUpdateAction(OnUIUpdate);

#if DEBUG
        QT.AddTab("Dev", DrawQtDev);
#endif
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("日常模式", DrawDailyMode);
        // QT.AddTab("反馈建议", UIHelp.Feedback);
        Dictionary<string, bool> qtDict = PLDSettings.Instance.MyQtDict;

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        QT.MyAddQt(qtDict, BaseQTKey.停手, false, "是否使用基础的Gcd");
        QT.MyAddQt(qtDict, BaseQTKey.爆发药, false);
        QT.MyAddQt(qtDict, BaseQTKey.突进, true);
        QT.MyAddQt(qtDict, PLDQTKey.战逃安魂, true);
        QT.MyAddQt(qtDict, PLDQTKey.大宝剑连击, true,"没有学习大保健用圣灵代替");
        QT.MyAddQt(qtDict, PLDQTKey.远程投盾, false, "和目标距离过远的时候使用");
        QT.MyAddQt(qtDict, PLDQTKey.远程圣灵, true, "和目标距离过远的时候使用");
        QT.MyAddQt(qtDict, PLDQTKey.即刻战逃, false, "战逃好了就用");
        QT.MyAddQt(qtDict, PLDQTKey.优先强化圣灵, false, "有强化圣灵的时候用");
        QT.MyAddQt(qtDict, PLDQTKey.优先圣灵, false, "有蓝就用");
        QT.MyAddQt(qtDict, PLDQTKey.优先赎罪, false);
        QT.MyAddQt(qtDict, PLDQTKey.厄运流转, true);
        QT.MyAddQt(qtDict, PLDQTKey.深奥之灵, true);
        QT.MyAddQt(qtDict, BaseQTKey.倾泻资源, false);
        QT.MyAddQt(qtDict, PLDQTKey.起手序列, false);
        QT.MyAddQt(qtDict, BaseQTKey.AOE, true);

        PLDSettings.Instance.JobViewSave.QtUnVisibleList.Clear();
        PLDSettings.Instance.JobViewSave.QtUnVisibleList.Add(PLDQTKey.优先圣灵);
        PLDSettings.Instance.JobViewSave.QtUnVisibleList.Add(BaseQTKey.AOE);

        QT.AddHotkey("LB", new HotKeyResolver_LB());
        // QT.AddHotkey("钢铁信念",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.钢铁信念, SpellTargetType.Self));
        // QT.AddHotkey("铁壁",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.铁壁, SpellTargetType.Self));
        // QT.AddHotkey("预警",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.预警.OriginalHook().Id, SpellTargetType.Self));
        // QT.AddHotkey("壁垒",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.壁垒, SpellTargetType.Self));
        // QT.AddHotkey("圣盾阵",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.圣盾阵, SpellTargetType.Self));
        // QT.AddHotkey("干预",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.干预, SpellTargetType.Pm2));
        // QT.AddHotkey("圣光幕帘",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.圣光幕帘, SpellTargetType.Self));
        // QT.AddHotkey("神圣领域",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.神圣领域, SpellTargetType.Self));
        //
        // QT.AddHotkey("亲疏自行",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.亲疏自行, SpellTargetType.Self));
        // QT.AddHotkey("雪仇",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.雪仇, SpellTargetType.Self));
        // QT.AddHotkey("退避",new HotKeyResolver_NormalSpell(PLDBaseSlotResolvers.退避, SpellTargetType.Pm2));
        QT.AddHotkey("一键减伤", new 一键减伤());

    }

    private void DrawDailyMode(JobViewWindow obj)
    {
        var pldSettings = PLDSettings.Instance;
        ImGui.Text("日常模式会持续开盾，和自动减伤");
        ImGui.SetNextItemWidth(150f);
        ImGui.Checkbox("启用", ref pldSettings.日常模式);
        ImGui.SetNextItemWidth(150f);
        ImGui.Checkbox("使用挑衅", ref pldSettings.挑衅);
        // ImGui.SetNextItemWidth(150f);
        ImGui.Checkbox("日常模式-残血不打爆发[测试中]", ref pldSettings.日常模式_残血不打爆发);
        ImGui.Spacing();
    }

    private IOpener? GetOpener(uint level)
    {
        if (level >= 64)
        {
            return new PLD_Opener();
        }
        return null;
    }

    public void OnUIUpdate()
    {
    }

    public void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        PLDSettingUI.BaseDraw();
    }

    public void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画Dev信息");

        // var dutySchedule = Core.Resolve<MemApiDuty>().GetSchedule();
        // ImGui.Text($"CountPoint : {dutySchedule.CountPoint}");
        // ImGui.Text($"NowPoint : {dutySchedule.NowPoint}");

        // ImGui.Text($"大保健连击Confiteor_GetResourceCost : {BaseIslotResolver.GetResourceCost(PLDBaseSlotResolvers.大保健连击Confiteor)}");
        // ImGui.Text($"大保健连击Confiteor.IsUnlock : {PLDBaseSlotResolvers.大保健连击Confiteor.IsUnlock()}");
        // ImGui.Text($"圣灵HolySpirit.ActionReady() : {PLDBaseSlotResolvers.圣灵HolySpirit.ActionReady()}");
        ImGui.Text($"lastComboActionID : {PLDBaseSlotResolvers.lastComboActionID}");
        ImGui.Text($"GetNearbyEnemyCount : {TargetHelper.GetNearbyEnemyCount(5)}");
        // ImGui.Text($"GCD : {GCDHelper.GetGCDCooldown()}");


        ImGui.Text($"挑衅 : {TankBaseIslotResolver.挑衅.ActionReadyAE()}");
        if (Core.Me.TargetObject is IBattleChara currTarget)
        {
            var 仇恨是否在自己身上 = currTarget.仇恨是否在自己身上();

            ImGui.Text($"CanAttack : {currTarget.CanAttack()}");
            ImGui.Text($"TargetObjectId : {currTarget.TargetObjectId}");
            ImGui.Text($"IsDead : {currTarget.IsDead}");
            ImGui.Text($"IsValid : {currTarget.IsValid()}");

            ImGui.Text($"仇恨是否在自己身上 : {仇恨是否在自己身上}");
            ImGui.Text($"IsTargetTTK12000 : {TTKHelper.IsTargetTTK(currTarget, 12000, true)}");
            ImGui.Text($"IsTargetTTK12 : {TTKHelper.IsTargetTTK(currTarget, 12, true)}");
        }


        ImGui.Text($"挑衅 : {TankBaseIslotResolver.挑衅.ActionReady()}");


        ImGui.Text($"挑衅 : {TankBaseIslotResolver.挑衅.ActionReady()}");
        ImGui.Text($"挑衅 : {TankBaseIslotResolver.挑衅.GetCooldownRemainingTime()}");
        ImGui.Text($"调停Intervene.Charges : {PLDBaseSlotResolvers.调停Intervene.Charges()}");
        ImGui.Text($"战斗时间1 : {CombatTime.Instance.combatStart}");
        ImGui.Text($"战斗时间2 : {CombatTime.Instance.combatEnd}");
        ImGui.Text($"战斗时间3 : {CombatTime.Instance.CombatEngageDuration().TotalSeconds}");
        ImGui.Text($"DistanceToPlayer : {Core.Me.TargetObject.DistanceToPlayer()}");
        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            ImGui.Text($"DistanceToPlayer : {TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me)}");
            ImGui.Text($"Distance : {Core.Me.GetCurrTarget()?.Distance(Core.Me)}");
            ImGui.Text($"Distance : {Core.Me.Distance(battleChara)}");
        }


        if (ImGui.TreeNode("小队"))
        {
            ImGui.Text($"周围小队成员数量：{PartyHelper.Party.Count}");
            ImGui.Text("小队成员:");
            if (PartyHelper.Party.Count > 0)
            {
                foreach (var t in PartyHelper.Party)
                {
                    ImGui.Separator();
                    ImGui.Text($"姓名:{t.Name}");
                    // ImGui.Text($"战斗状态:{Helper.目标战斗状态(t)}");
                    ImGui.Text($"死亡状态:{t.IsDead}");
                    // ImGui.Text($"距离:{t.Distance(Helper.自身)}");
                }
            }
            ImGui.TreePop();
        }

        // ImGui.Text($"大保健连击Confiteor : {PLDBaseSlotResolvers.大保健连击Confiteor.OriginalHook().Id}");
        // ImGui.Text($"赎罪剑Atonement1 : {PLDBaseSlotResolvers.赎罪剑Atonement1.OriginalHook().Id}");
        // ImGui.Text($"GCD : {GCDHelper.GetGCDCooldown()}");


        // ImGui.Text($"圣灵buff : {BaseIslotResolver.GetBuffRemainingTime(PLDBaseSlotResolvers.Buffs.DivineMight)}");
        // ImGui.Text($"ActionReady : {PLDBaseSlotResolvers.深奥之灵SpiritsWithin.ActionReady()}");
        // ImGui.Text($"OriginalHook : {PLDBaseSlotResolvers.深奥之灵SpiritsWithin.OriginalHookActionReady()}");
        // ImGui.Text($"attackMeCount : {BaseIslotResolver.attackMeCount()}");
        // ImGui.Text($"ActionReadyww : {PLDBaseSlotResolvers.圣盾阵.ActionReady()}");
        // ImGui.Text($"OriginalHookActionReady : {PLDBaseSlotResolvers.圣盾阵.OriginalHookActionReady()}");
        // ImGui.Text($"OriginalHookActionReady1 : {PLDBaseSlotResolvers.GetCooldownRemainingTime(846)}");
        // ImGui.Text($"OriginalHookActionReady2 : {Core.Resolve<MemApiSpell>().GetCooldown(Spell.CreatePotion().Id).TotalSeconds}");
        // ImGui.Text($"OriginalHookActionReady3 : {PLDBaseSlotResolvers.GetCooldownRemainingTime(Spell.CreatePotion().Id)}");

        // if (ImGui.Button("测试"))
        // {
        //     NetworkHelper.SendStartBySelf(720915U);
        // }

    }
}