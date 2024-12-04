#region

using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.JobApi;
using Dalamud.Game.ClientState;
using Dalamud.Logging;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_嗜血 : DRKBaseSlotResolvers
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



        if (DKSettings.Instance.日常模式)
        {
            if (DKSettings.Instance.日常模式_残血不打爆发)
            {
                if (战斗爽() == false)
                {
                    return Flag_残血不打爆发;
                }
            }
        }


        if (!CanWeave())
        {
            return -1;
        }

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;
        if (darksideTimeRemaining == 0)
            return -2;

        if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
            return -1;

        if (嗜血BloodWeapon.ActionReady())
        {
            return 0;
        }

        if (血乱Delirium.ActionReady())
        {
            return 0;
        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        Spell spell = new Spell(嗜血BloodWeapon, Core.Me);
        slot.Add(spell);
    }
}