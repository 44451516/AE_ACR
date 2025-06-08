using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;
using FFXIVClientStructs.FFXIV.Client.Game;

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

    public static uint 爆发药Id()
    {
        var containsKey = SettingMgr.GetSetting<PotionSetting>().ChoosedPotion.ContainsKey(PotionType.Str);
        if (containsKey == false)
        {
            return 0;
        }
        
        return SettingMgr.GetSetting<PotionSetting>().ChoosedPotion[PotionType.Str];
    }

    public static unsafe int 爆发药数量()
    {
        var containsKey = SettingMgr.GetSetting<PotionSetting>().ChoosedPotion.ContainsKey(PotionType.Str);
        if (containsKey == false)
        {
            return 0;
        }
        
        uint id = SettingMgr.GetSetting<PotionSetting>().ChoosedPotion[PotionType.Str];

        if (id == 0)
            return 0;

        int itemCount = InventoryManager.Instance()->GetInventoryItemCount(id, true);

        return itemCount;
    }
}