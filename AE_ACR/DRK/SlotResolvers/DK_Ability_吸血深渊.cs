#region

using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_吸血深渊 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }
        
        if (getQTValue(BaseQTKey.攒资源))
        {
            return Flag_攒资源;
        }
        
        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }



        if (!CanWeave())
        {
            return -1;
        }

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0)
            return -2;


        var battleChara = Core.Me.GetCurrTarget();
        if (battleChara == null)
        {
            return -5;
        }

        if (TargetHelper.GetNearbyEnemyCount(battleChara, 5, 5) < 2)
            return -4;


        if (AbyssalDrain.ActionReady() && TargetHelper.GetNearbyEnemyCount(battleChara, 5, 5) >= 3)
            return 0;

        return -5;
    }


    public override void Build(Slot slot)
    {
        slot.Add(AbyssalDrain.OriginalHook());
    }
}