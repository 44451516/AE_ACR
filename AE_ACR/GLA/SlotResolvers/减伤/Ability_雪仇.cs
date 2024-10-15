#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.GLA.SlotResolvers.减伤;

public class Ability_雪仇 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (!雪仇.ActionReady())
        {
            return Flag_CD;
        }
        
        if (!雪仇.ActionReadyAE())
        {
            return Flag_CD;
        }

        
        if (CanWeave())
        {
            if (雪仇.ActionReady())
            {
                if (TargetHelper.GetNearbyEnemyCount(5) >= 4)
                {
                    return 0; 
                }
                
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.TargercastingIsDeathSentence(target, 3))
                    {
                        return 0;
                    }
                }
            }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(雪仇.OriginalHook());
    }
}