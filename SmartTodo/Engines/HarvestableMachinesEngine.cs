using Microsoft.Xna.Framework;
using SmartTodo.Components.TodoItems;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Engines
{
    internal class HarvestableMachinesEngine(
        Action<string, StardewModdingAPI.LogLevel> log,
        Func<bool> isEnabled
    ) : BaseEngine<HarvestableMachinesTodoItem>(log, isEnabled, Frequency.EveryTimeChange)
    {

        public override void UpdateItems()
        {
            // check if there are harvestable machine in various locations
            Utility.ForEachLocation((gameLocation) =>
            {
                if (gameLocation is null)
                {
                    return true;
                }

                int harvestableCount = gameLocation.getNumberOfMachinesReadyForHarvest();
                if (harvestableCount > 0)
                {
                    items.Add(new HarvestableMachinesTodoItem(gameLocation));
                }

                return true;
            });
        }
    }
}