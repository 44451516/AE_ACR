using System.Numerics;
using AE_ACR.PLD.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace AE_ACR.PLD.HotKey;

public class HotkeyResolver_挑衅:IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(PLDBaseSlotResolvers.挑衅, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(PLDBaseSlotResolvers.挑衅.GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (PLDBaseSlotResolvers.挑衅.GetSpell().IsReadyWithCanCast())
        {
            return 0;
        }
        
        return -1;
    }

    public void Run()
    {
        if (PLDBaseSlotResolvers.挑衅.GetSpell().IsReadyWithCanCast())
        {
            var slot = new Slot();
            Spell spell = new Spell(PLDBaseSlotResolvers.挑衅.GetSpell().Id, SpellTargetType.Target);
            slot.Add(spell);
            slot.Run(AI.Instance.BattleData, false);
        }
    }
}