using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EstherMod
{
    public class EstherModGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(1);
            recipe = Recipe.Create(ItemID.CrimsonKey);
            recipe.AddIngredient(ItemID.CorruptionKey, 1);
            recipe.Register();

            recipe = Recipe.Create(ItemID.CorruptionKey);
            recipe.AddIngredient(ItemID.CrimsonKey, 1);
            recipe.Register();

            recipe = Recipe.Create(ItemID.Ebonwood);
            recipe.AddIngredient(ItemID.Shadewood, 1);
            recipe.Register();

            recipe = Recipe.Create(ItemID.Shadewood);
            recipe.AddIngredient(ItemID.Ebonwood, 1);
            recipe.Register();

            recipe = Recipe.Create(ItemID.TrueNightsEdge);
            recipe.AddIngredient(Mod.Find<ModItem>("Esther").Type, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 20);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
            recipe.AddIngredient(ItemID.SoulofSight, 20);
            recipe.Register();
        }
    }
}
