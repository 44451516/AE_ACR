using AE_ACR_DRK_Setting;
using AE_ACR.PLD.Data;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_战逃反应内 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (HasEffect(Buffs.FightOrFlight))
        {
            return 0;
        }
        
        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        

        return Data.Data.先锋剑FastBlade.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        Spell spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}