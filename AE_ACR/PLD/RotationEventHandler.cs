#region

using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD;

/// <summary>
///     事件回调处理类 参考接口里的方法注释
/// </summary>
public class RotationEventHandler : IRotationEventHandler
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
        PLDRotationEntry.QT.Reset();
    }

    public async Task OnNoTarget()
    {
        // await Task.CompletedTask;
    }

    public void OnSpellCastSuccess(Slot slot, Spell spell)
    {
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
    }

    public void OnBattleUpdate(int currTimeInMs)
    {
        if (CombatTime.Instance.combatStart == DateTime.MinValue) CombatTime.Instance.combatStart = DateTime.Now;

        CombatTime.Instance.UpdateCombatTimer();
    }

    public void OnEnterRotation()
    {
    }

    public void OnExitRotation()
    {
    }

    public void OnTerritoryChanged()
    {
        if (Data.IsInHighEndDuty)
        {
            PLDSettings.Instance.日常模式 = false;
        }
    }
}