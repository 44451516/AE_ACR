using AEAssist;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;

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
        if (GameObjectExtension.HasAura(Core.Me, 强化药, 0)) return true;

        if (GameObjectExtension.HasAura(Core.Me, 灼热之光, 0)) return true;

        if (GameObjectExtension.HasAura(Core.Me, 技巧舞步结束TechnicalFinish, 0)) return true;

        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (GameObjectExtension.HasAura(battleChara, 攻其不备, 0)) return true;

            if (GameObjectExtension.HasAura(battleChara, 连环计, 0)) return true;
        }

        return false;
    }
}