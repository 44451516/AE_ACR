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
using AEAssist.JobApi;
using AEAssist.MemoryApi;
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
        new SlotResolverData(new GCD_优先圣灵(), SlotMode.Gcd),
        new SlotResolverData(new GCD_优先赎罪(), SlotMode.Gcd),
        new SlotResolverData(new GCD_沥血剑(), SlotMode.Gcd),
        new SlotResolverData(new GCD_大宝剑连击(), SlotMode.Gcd),
        new SlotResolverData(new PLD_GCD_远程圣灵(), SlotMode.Gcd),
        new SlotResolverData(new PLD_GCD_投盾(), SlotMode.Gcd),
        new SlotResolverData(new GCD_Base(), SlotMode.Gcd)
    };

    public static JobViewWindow QT { get; private set; }
    public string AuthorName { get; set; } = "44451516";

    public Rotation Build(string settingFolder)
    {
#if DEBUG
        AELoggerUtils.init();
#endif


        // 初始化设置
        PLDSettings.Build(settingFolder);
        // 初始化QT （依赖了设置的数据）
        BuildQT();

        var rot = new Rotation(SlotResolvers)
        {
            // TargetJob = Jobs.Marauder,
            // TargetJob = Jobs.Gladiator,
            // TargetJob = Jobs.WhiteMage,
            TargetJob = Jobs.Paladin,
            AcrType = AcrType.Both,
            MinLevel = 1,
            MaxLevel = 100,
            Description = "[战逃安魂]控制着所有的爆发\n"
                          + "[优先圣灵]满足圣灵的释放条件会一直用圣灵\n"
                          + "[优先赎罪]满足赎罪的释放条件会一直用赎罪\n"
                          + "[远程圣灵]拥有强化圣灵or达到设置阈值且不移动的时候使用\n"
                          + "[即刻战逃]会立刻使用战逃，即使没有合适的资源\n"
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
        // 释放需要释放的东西 没有就留空
    }

    // 构造函数里初始化QT
    public void BuildQT()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        QT = new JobViewWindow(PLDSettings.Instance.JobViewSave, PLDSettings.Instance.Save, "PLD");
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
#if DEBUG
        QT.AddTab("Dev", DrawQtDev);
#endif
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("日常模式", DrawDailyMode);
        // QT.AddTab("反馈建议", UIHelp.Feedback);

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        QT.AddQt(BaseQTKey.停手, false, "是否使用基础的Gcd");
        QT.AddQt(BaseQTKey.爆发药, false);
        QT.AddQt(BaseQTKey.突进, true);
        QT.AddQt(PLDQTKey.战逃安魂, true);
        QT.AddQt(PLDQTKey.大宝剑连击, true);
        QT.AddQt(PLDQTKey.远程投盾, false, "和目标距离过远的时候使用");
        QT.AddQt(PLDQTKey.远程圣灵, true, "和目标距离过远的时候使用");
        QT.AddQt(PLDQTKey.即刻战逃, false, "战逃好了就用");
        QT.AddQt(PLDQTKey.优先圣灵, false);
        QT.AddQt(PLDQTKey.优先赎罪, false);
        QT.AddQt(PLDQTKey.厄运流转, true);
        QT.AddQt(PLDQTKey.深奥之灵, true);
        QT.AddQt(PLDQTKey.起手序列, false);
        // QT.AddQt(BaseQTKey.起手序列突进, false);

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
        QT.AddHotkey("一键减伤",new 一键减伤());

        
        // QT.AddQt(BaseQTKey.减伤, true);
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
        if (level>= 64)
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

        var dutySchedule = Core.Resolve<MemApiDuty>().GetSchedule();
        ImGui.Text($"CountPoint : {dutySchedule.CountPoint}");
        ImGui.Text($"NowPoint : {dutySchedule.NowPoint}");
        
        
        
        
        var Oath = Core.Resolve<JobApi_Paladin>().Oath;

        ImGui.Text($"挑衅 : {TankBaseIslotResolver.挑衅.ActionReadyAE()}");
        if (Core.Me.TargetObject is IBattleChara currTarget)
        {
            var 仇恨是否在自己身上 = currTarget.仇恨是否在自己身上();
            
            ImGui.Text($"CanAttack : {currTarget.CanAttack()}");
            ImGui.Text($"TargetObjectId : {currTarget.TargetObjectId}");
            ImGui.Text($"IsDead : {currTarget.IsDead }");
            ImGui.Text($"IsValid : {currTarget.IsValid() }");
            
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