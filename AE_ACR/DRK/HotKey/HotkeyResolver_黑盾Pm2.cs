using System.Numerics;
using AE_ACR.DRK.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace AE_ACR_DRK.HotKey;

public class HotkeyResolver_黑盾Pm2 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(DRKBaseSlotResolvers.至黑之夜, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(DRKBaseSlotResolvers.至黑之夜.GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (DRKBaseSlotResolvers.至黑之夜.GetSpell().IsReadyWithCanCast())
        {
            return 0;
        }
        return -1;
    }

    public void Run()
    {
        var slot = new Slot();
        slot.Add(new Spell(DRKBaseSlotResolvers.至黑之夜.GetSpell().Id, SpellTargetType.Pm2));
        slot.Run(AI.Instance.BattleData, false);

        // if (AI.Instance.BattleData.NextSlot == null)
        //     AI.Instance.BattleData.NextSlot = new Slot();
        //
        // if (DRKBaseSlotResolvers.至黑之夜.GetSpell().IsReadyWithCanCast())
        //     AI.Instance.BattleData.NextSlot.Add(new Spell(DRKBaseSlotResolvers.至黑之夜.GetSpell().Id, SpellTargetType.Pm2));
    }
}