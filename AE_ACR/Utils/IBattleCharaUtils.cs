using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.utils;

internal static class BattleCharaUtils
{
    public static bool 仇恨是否在自己身上(this IBattleChara battleChara)
    {
        if (battleChara.CanAttack() && battleChara.TargetObjectId != 0 && battleChara.IsDead == false && battleChara.IsValid() == true)
        {
            return battleChara.TargetObjectId == Core.Me.GameObjectId;
        }
        return false;
    }
}