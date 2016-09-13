using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PoGo.NecroBot.Logic.Common;
using PoGo.NecroBot.Logic.Event;
// using PoGo.NecroBot.Logic.Logging;
using PoGo.NecroBot.Logic.State;

namespace PoGo.NecroBot.Logic.Tasks
{
    public class GetPokeDexList
    {
        public static async Task Execute(ISession session, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await session.Inventory.RefreshCachedInventory();

            var PokeDex = await session.Inventory.GetPokeDexItems();
            
            session.EventDispatcher.Send(
                new PokeDexListEvent
                {
                    Items = PokeDex.ToList()
                });

            // var _totalUniqueEncounters = PokeDex.Select(i => new { Pokemon = i.InventoryItemData.PokedexEntry.PokemonId, Captures = i.InventoryItemData.PokedexEntry.TimesCaptured });
            // var _totalCaptures = _totalUniqueEncounters.Count(i => i.Captures > 0);
            // var _totalData = PokeDex.Count();
            
            // Logger.Write(session.Translation.GetTranslation(TranslationString.AmountPkmSeenCaught, _totalData, _totalCaptures));

            DelayingUtils.Delay(session.LogicSettings.DelayBetweenPlayerActions, 0);
        }
    }
}