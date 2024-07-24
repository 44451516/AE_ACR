#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class GCD_生辰 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }


        var 目标 = PartyHelper.DeadAllies.FirstOrDefault(r => r.CurrentHp == 0 || r.IsDead);

        if (目标 != null && 目标.IsValid())
        {
            return 0;
        }

        return -1;
    }



    public override void Build(Slot slot)
    {
        if (即刻咏唱.ActionReady())
        {
            slot.Add(即刻咏唱.OriginalHook());
        }

        var 目标 = PartyHelper.DeadAllies.FirstOrDefault(r => r.CurrentHp == 0 || r.IsDead);

        if (目标 != null && 目标.IsValid())
        {
            slot.Add(new Spell(生辰, 目标));
        }

    }
}