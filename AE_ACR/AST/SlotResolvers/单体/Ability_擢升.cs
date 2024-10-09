#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_擢升 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (擢升.ActionReady())
            {

                var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
                    .Where(r => r.CurrentHp > 0 && r.CurrentHpPercent() <= 0.8f && r.IsTank()).OrderBy(r => r.CurrentHpPercent()) //排序
                    .FirstOrDefault();

                if (目标 != null && 目标.IsValid())
                {

                    return 1;
                }
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(new Spell(擢升, getTankHpOrderByPercent()));
    }
}