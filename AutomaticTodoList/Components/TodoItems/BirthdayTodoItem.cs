using AutomaticTodoList.Models;
using StardewModdingAPI.Events;
using StardewValley;

namespace AutomaticTodoList.Components.TodoItems
{
    /// <summary>A birthday todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="BirthdayTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal class BirthdayTodoItem : BaseTodoItem
    {
        internal NPC NPC { get; }

        public BirthdayTodoItem(NPC npc, bool isChecked = false)
            : base("", isChecked, 100)
        {
            this.NPC = npc;
            this.Text = $"Give {npc.getName()} a birthday gift";
        }

        public override void OnUpdateTicked(UpdateTickedEventArgs e)
        {
            if (!IsChecked)
            {
                if (Game1.player.friendshipData[this.NPC.Name].GiftsToday > 0)
                {
                    this.MarkCompleted();
                }
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is BirthdayTodoItem otherItem && this.NPC.Name == otherItem.NPC.Name;
        }

        public override int GetHashCode()
        {
            return (this.GetType(), this.NPC.Name).GetHashCode();
        }
    }
}