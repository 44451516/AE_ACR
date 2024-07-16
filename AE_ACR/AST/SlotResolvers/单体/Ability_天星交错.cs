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

public class Ability_天星交错 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
                .Where(r => r.CurrentHp > 0 && r.CurrentHpPercent() <= 0.8f && r.IsTank()).OrderBy(r => r.CurrentHpPercent()) //排序
                .FirstOrDefault();

            if (目标 != null && 目标.IsValid())
            {
                if (天星交错.ActionReady())
                {
                    return 1;
                }
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(new Spell(天星交错, getTankHpOrderByPercent()));
    }
}