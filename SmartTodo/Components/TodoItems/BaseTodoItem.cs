using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SmartTodo.Models;
using StardewValley;

namespace SmartTodo.Components.TodoItems
{

    /// <summary>A todo item.</summary>
    /// <remarks>Initializes a new instance of the <see cref="BaseTodoItem"/> class.</remarks>
    /// <param name="text">The text of the todo item.</param>
    internal abstract class BaseTodoItem(string text = "", bool isChecked = false, int priority = 0, Action<ITodoItem>? addToCompletedCache = null) : ITodoItem
    {

        /// <summary>The text of the todo item.</summary>
        public string Text { get; set; } = text;

        /// <summary>The checkbox state of the todo item.</summary>
        public bool IsChecked { get; set; } = isChecked;

        /// <summary>The priority of the todo item. A higher number means a higher priority.</summary>
        public int Priority { get; set; } = priority;

        internal Action<ITodoItem>? AddToCompletedCache { get; } = addToCompletedCache;

        private static readonly Lazy<Texture2D> LazyPixel = new(() =>
        {
            Texture2D pixel = new(Game1.graphics.GraphicsDevice, 1, 1);
            pixel.SetData([Color.White]);
            return pixel;
        });

        /// <summary>A blank pixel which can be colorized and stretched to draw geometric shapes.</summary>
        public static Texture2D Pixel => LazyPixel.Value;

        public virtual void MarkCompleted()
        {
            this.IsChecked = true;
            if (this.AddToCompletedCache is not null)
            {
                this.AddToCompletedCache(this);
            }
        }

        public virtual void OnUpdateTicked() { }

        public void Draw(SpriteBatch b, Vector2 position)
        {
            SpriteFont font = Game1.smallFont;
            Color textColor = Game1.textColor;

            Utility.drawTextWithShadow(
                b,
                this.Text,
                font,
                position,
                textColor
            );

            if (this.IsChecked)
            {
                Vector2 textSize = font.MeasureString(this.Text);
                b.Draw(
                    Pixel,
                    new Rectangle(
                        (int)position.X,
                        (int)(position.Y + textSize.Y / 2),
                        (int)textSize.X,
                        1
                    ),
                    Color.Black
                );
            }
        }
    }
}