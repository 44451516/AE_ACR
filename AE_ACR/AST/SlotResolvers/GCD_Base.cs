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
        if (是否停手())
        {
            return Flag_停手;
        }

        return 0;
    }

    private Spell GetAOEGCDSpell()
    {
        var battleChara = Core.Me.GetCurrTarget();
        if (AOE.OriginalHook().IsUnlock() && TargetHelper.GetNearbyEnemyCount(battleChara, 25, 5) >= 2)
        {
            return AOE.OriginalHook();
        }


        List<uint> 排除buff = new List<uint>
        {
            838,
            843,
            1881
        };

        if (battleChara.IsBoss() && battleChara.CurrentHpPercent() > 0.1f)
        {
            if (!battleChara.HasAnyAura(排除buff))
            {
                return dot.OriginalHook();
            }
        }

        return 落陷凶星.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}