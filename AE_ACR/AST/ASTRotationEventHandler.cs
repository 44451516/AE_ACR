#region

using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.AST;

/// <summary>
///     事件回调处理类 参考接口里的方法注释
/// </summary>
public class ASTRotationEventHandler : IRotationEventHandler
{
    public async Task OnPreCombat()
    {
        CombatTime.Instance.combatEnd = DateTime.MinValue;
        CombatTime.Instance.combatStart = DateTime.MinValue;
    }

    public void OnResetBattle()
    {
        // 重置战斗中缓存的数据
        CombatTime.Instance = new CombatTime();
        // QT的设置重置为默认值
        // ASTRotationEntry.QT.Reset();
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
        if (CombatTime.Instance.combatStart == DateTime.MinValue)
            CombatTime.Instance.combatStart = DateTime.Now;

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
    }
}