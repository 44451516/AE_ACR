#region

using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.PLD.Triggers;
using AE_ACR.utils;
using AE_ACR.Utils;
using AE_ACR.utils.Triggers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.Helper;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.PLD;

/// <summary>
///     事件回调处理类 参考接口里的方法注释
/// </summary>
public class PLDRotationEventHandler : IRotationEventHandler
{
    public async Task OnPreCombat()
    {
        CombatTime.Instance.combatEnd = DateTime.MinValue;
        CombatTime.Instance.combatStart = DateTime.MinValue;
        if (PLDSettings.Instance.日常模式)
        {
            if (!BaseIslotResolver.HasEffect(PLDBaseSlotResolvers.Buffs.钢铁信念) && PLDBaseSlotResolvers.钢铁信念.ActionReady())
            {
                var slot = new Slot();
                slot.Add(PLDBaseSlotResolvers.钢铁信念.GetSpell());
                await slot.Run(AI.Instance.BattleData, false);
            }
        }

    }

    public void OnResetBattle()
    {
        // 重置战斗中缓存的数据
        CombatTime.Instance = new CombatTime();
        // QT的设置重置为默认值
        // PLDRotationEntry.QT.Reset();

        TriggerAction_大翅膀_Rot.Start = false;
        TriggerAction_M1S_Rot.Start = false;
        PLDRotationEntry.QT.SetQt(PLDQTKey.大翅膀最优面向, false);
    }

    public async Task OnNoTarget()
    {
        if (PLDSettings.Instance.上天战逃)
        {
            if (PLDBaseSlotResolvers.战逃反应FightOrFlight.ActionReady())
            {
                if (CombatTime.Instance.CombatEngageDuration().TotalSeconds >= PLDSettings.Instance.上天战逃开始时间
                    && CombatTime.Instance.CombatEngageDuration().TotalSeconds <= PLDSettings.Instance.上天战逃结束时间)
                {
                    var slot = new Slot();
                    Spell spell = new Spell(PLDBaseSlotResolvers.战逃反应FightOrFlight, Core.Me);
                    slot.Add(spell);
                    await slot.Run(AI.Instance.BattleData, false);
                }
            }
        }
    }

    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
    }

    public void OnBattleUpdate(int currTimeInMs)
    {
        if (CombatTime.Instance.combatStart == DateTime.MinValue)
            CombatTime.Instance.combatStart = DateTime.Now;

        CombatTime.Instance.UpdateCombatTimer();
        if (TriggerAction_M1S_Rot.Start && Core.Resolve<MemApiZoneInfo>().GetCurrTerrId() == 1226U)
        {
            RotUtil.M1S_FaceFarPointInSquare(Core.Me.Position);
        }

        if (PLDRotationEntry.QT.GetQt(PLDQTKey.大翅膀最优面向_测试) || TriggerAction_大翅膀_Rot.Start)
        {
            RotUtil.骑士大翅膀FaceFarPoint();
        }


        if (PLDRotationEntry.QT.GetQt(PLDQTKey.大翅膀最优面向))
        {
            if (PLDBaseSlotResolvers.大翅膀.GetSpell().IsReadyWithCanCast())
            {

                RotUtil.骑士大翅膀FaceFarPoint();
                var slot = new Slot();
                slot.Add(new Spell(PLDBaseSlotResolvers.大翅膀.GetSpell().Id, SpellTargetType.Self));
                slot.Run(AI.Instance.BattleData, false);
            }
        }

        // if (TriggerAction_大翅膀_Rot.Start )
        // {
        //     RotUtil.骑士大翅膀FaceFarPoint();
        // }
        //


    }

    public void OnEnterRotation()
    {
    }

    public void OnExitRotation()
    {
    }

    public void OnTerritoryChanged()
    {
        if (PLDRotationEntry.QT != null)
        {
            PLDRotationEntry.QT.SetQt(PLDQTKey.大翅膀最优面向, false);
            PLDRotationEntry.QT.SetQt(PLDQTKey.大翅膀最优面向_测试, false);
            TriggerAction_大翅膀_Rot.Start = false;
            TriggerAction_M1S_Rot.Start = false;
        }


        if (Data.IsInHighEndDuty)
        {
            PLDSettings.Instance.日常模式 = false;
        }

        if (Core.Resolve<MemApiZoneInfo>().GetCurrTerrId() == 1238)
        {
            if (PLDSettings.Instance.绝伊甸设置)
            {
                LogHelper.Print("ACR:进入LGBT讨伐战，自动开启上天战逃");
                PLDSettings.Instance.上天战逃 = true;
                PLDSettings.Instance.上天战逃开始时间 = 30;
                PLDSettings.Instance.上天战逃结束时间 = 60 * 5 + 30; 
            }
            
        }
    }
}