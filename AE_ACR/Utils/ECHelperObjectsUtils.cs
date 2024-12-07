using AEAssist;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.Utils;

public class ECHelperObjectsUtils
{
    public static List<IBattleChara> get8()
    {
        List<IBattleChara> list = new List<IBattleChara>();
        foreach (var gameObject in ECHelper.Objects)
        {
            if (gameObject is IBattleChara battleChara)
            {
                var unitDis = Core.Me.Distance(gameObject);
                if (unitDis < 8)
                {
                    list.Add(battleChara);
                }
            }

        }

        return list;
    }
}