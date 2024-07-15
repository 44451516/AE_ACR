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

public class GCD_奶自己 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        

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
        return 福星.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}