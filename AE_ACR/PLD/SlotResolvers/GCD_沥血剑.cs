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

public class GCD_沥血剑 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (HasEffect(Buffs.沥血剑BUFFGoringBladeReady))
        {
            return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(沥血剑GoringBlade.OriginalHook());
    }
}