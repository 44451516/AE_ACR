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

public class Ability_先天禀赋 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {


            if (先天禀赋.ActionReady())
            {
                var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
                    .Where(r => r.CurrentHp > 0 && r.CurrentHpPercent() <= 0.5f && r.IsTank()).OrderBy(r => r.CurrentHpPercent()) //排序
                    .FirstOrDefault();
                if (目标 != null && 目标.IsValid())
                {

                    return 1;
                }

                if (Core.Me.CurrentHpPercent() <= 0.5f)
                {
                    return 0;
                }
            }

        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        if (Core.Me.CurrentHpPercent() <= 0.5f)
        {
            slot.Add(先天禀赋.OriginalHook());
        }
        else
        {
            // slot.Add(new Spell(先天禀赋, getTankHpOrderByPercent));
            slot.Add(new Spell(先天禀赋, getTankHpOrderByPercent()));
        }


    }
}