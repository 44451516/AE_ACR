#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_优先赎罪 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }

        if (getQTValue(PLDQTKey.优先赎罪))
        {
            if (赎罪剑Atonement.MyIsUnlock())
            {
                if (HasEffect(Buffs.赎罪剑Atonement1BUFF) || HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF))
                {
                    return 0;
                }
            }

        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(赎罪剑Atonement.OriginalHook());
    }
}