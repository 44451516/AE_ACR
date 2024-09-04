// using AEAssist;
// using AEAssist.Avoid;
// using AEAssist.Helper;
// using AEAssist.MemoryApi;
// using Dalamud.Game.ClientState.Objects.Types;
// using Trust.DungeonController.Dungeon;
// using Trust.DungeonController.SpellReaction;
//
// namespace ScriptTest;
//
// public class Trust97老1脚本_小怪_钢铁_36570 : IResolverScript
// {
//     public void OnActive(DungeonController dungeonController, ResolverCondParams spellCondParams)
//     {
//
//     }
//
//     public void OnUpdate(DungeonController dungeonController)
//     {
//         // 先拿到当前场景的地图管理
//         var mapBaseName = Core.Resolve<MemApiZoneInfo>().GetCurrZoneInfo().MapBaseName;
//         if (!AvoidManager.Instance.GetMap(mapBaseName, out var map))
//         {
//
//             return;
//         }
//
//
//         foreach (var gameObject in ECHelper.Objects)
//         {
//             if (gameObject.DataId != 16828)
//                 continue;
//
//             if (gameObject is IBattleChara battleChara)
//             {
//                 if (battleChara.CastActionId == 36570)
//                 {
//                     if (battleChara.CurrentCastTime >= 7 && battleChara.CurrentCastTime <= 7.5f)
//                     {
//                         map.AddDangerShape
//                         (
//                             gameObject.EntityId, new DangerShape
//                             (
//                                 new CircleShape()
//                                 {
//                                     Name = $"DangerFromPM {gameObject.Name}",
//                                     Origin = gameObject.Position,
//                                     Radius = 18f
//                                 }, TimeHelper.Now(), 2000
//                             ), true
//                         );
//                     }
//                 }
//
//             }
//
//
//         }
//
//         // 让角色移动到安全区
//         SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
//     }
// }