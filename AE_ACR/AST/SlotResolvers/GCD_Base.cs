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

public class GCD_Base : ASTBaseSlotResolvers
{
    public override int Check()
    {
        return 0;
    }

    private Spell GetAOEGCDSpell()
    {
        var battleChara = Core.Me.GetCurrTarget();
        if (TargetHelper.GetNearbyEnemyCount(battleChara, 25, 5) >= 2)
        {
            return AOE.OriginalHook();
        }
        return 落陷凶星.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}