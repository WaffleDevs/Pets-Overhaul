using Microsoft.Xna.Framework;
using System.Collections.Generic;

using PetsOverhaul.UI;

using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace PetsOverhaul
{
    public class PetsOverhaul : Mod
    {
        private UserInterface _exampleUserInterface;

        internal UserInterface ExamplePersonUserInterface;

        public override void Load()
        {
            ExamplePersonUserInterface = new UserInterface();
            base.Load();
        }
        public void UpdateUI(GameTime gameTime)
        {
            ExamplePersonUserInterface?.Update(gameTime);
        }

        public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Person UI",
                    delegate
                    {
                        // If the current UIState of the UserInterface is null, nothing will draw. We don't need to track a separate .visible value.
                        ExamplePersonUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}