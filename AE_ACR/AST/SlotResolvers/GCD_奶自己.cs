#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class GCD_奶自己 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (吉星.MyIsUnlock() == false)
        {
            return Flag_没有解锁;
        }

        if (Core.Me.CurrentHpPercent() <= 0.99f)
        {
            if (attackMeCount() > 0)
            {
                return 0;
            }
        }
        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        if (福星.OriginalHook().MyIsUnlock())
        {
            return 福星.OriginalHook();
        }
        return 吉星.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}