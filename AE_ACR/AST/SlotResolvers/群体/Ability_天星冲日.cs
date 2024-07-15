#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_天星冲日 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
            .Where(r => r.CurrentHp > 0 && !r.IsTank() && r.CurrentHpPercent() <= 0.85f) //且 不具有几个buff 且不具有list中的buff 3秒
            .OrderBy(r => r.CurrentHpPercent()) //排序
            .FirstOrDefault();

        if (目标 != null && 目标.IsValid())
        {
            if (天星冲日.ActionReady())
            {
                return 0;
            }
        }


        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        return 天星冲日.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}