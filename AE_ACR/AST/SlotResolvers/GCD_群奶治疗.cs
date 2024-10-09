#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class GCD_群奶治疗 : ASTBaseSlotResolvers
{
    public override int Check()
    {

        if (是否停手())
        {
            return Flag_停手;
        }

        var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
            .Where(r => r.CurrentHp > 0 && !r.IsTank()) //且 不具有几个buff 且不具有list中的buff 3秒
            .OrderBy(r => r.CurrentHpPercent()) //排序
            .FirstOrDefault();

        if (目标 != null && 目标.IsValid())
        {
            if (目标.CurrentHpPercent() <= 0.5f)
            {
                return 0;
            }


            if (目标.CurrentHpPercent() <= 0.85f)
            {
                return 0;
            }

        }

        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        if (HasEffect(Buffs.医技buff) || HasEffect(Buffs.医技buff2))
        {
            return 阳星.OriginalHook();
        }
        
        if (阳星相位.IsUnlock())
        {
            return 阳星相位.OriginalHook();
        }

        return 阳星.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}