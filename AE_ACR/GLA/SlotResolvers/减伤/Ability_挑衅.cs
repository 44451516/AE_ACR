#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;

#endregion

namespace AE_ACR.GLA.SlotResolvers.减伤;

public class Ability_挑衅 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (挑衅.GetCooldownRemainingTime() != 0)
        {
            return Flag_CD;
        }
        
        if (comboTime >= 5)
        {
            if (挑衅.ActionReady())
            {
                foreach (var keyValuePair in TargetMgr.Instance.EnemysIn25)
                {
                    var battleChara = keyValuePair.Value;
                    if (battleChara.CanAttack() && battleChara.TargetObjectId != 0 && battleChara.TargetObjectId != Core.Me.GameObjectId)
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
        foreach (var keyValuePair in TargetMgr.Instance.EnemysIn25)
        {
            var battleChara = keyValuePair.Value;
            if (battleChara.CanAttack() && battleChara.TargetObjectId != 0 && battleChara.TargetObjectId != Core.Me.GameObjectId)
            {
                slot.Add(new Spell(挑衅, battleChara));
            }
        }


    }
}