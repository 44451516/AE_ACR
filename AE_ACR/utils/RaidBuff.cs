#region

using AEAssist;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.utils;

public class RaidBuff
{
    private const ushort

        // Heals
        强化药 = 49,
        灼热之光 = 2703,
        连环计 = 1221,

        //忍者夺取
        攻其不备 = 3254,
        受伤加重 = 638,
        技巧舞步结束TechnicalFinish = 1822,
        留空 = 0;


    public static bool 爆发期()
    {
        if (Core.Me.HasAura(强化药)) return true;

        if (Core.Me.HasAura(灼热之光)) return true;

        if (Core.Me.HasAura(技巧舞步结束TechnicalFinish)) return true;

        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (battleChara.HasAura(攻其不备)) return true;

            if (battleChara.HasAura(连环计)) return true;
        }

        return false;
    }
}