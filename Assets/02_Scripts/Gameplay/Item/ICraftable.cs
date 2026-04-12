
using System.Collections.Generic;

namespace Elemental.Gameplay.Item
{
    public interface ICraftable
    {
        bool CanCraft(List<RecipeIngredient> ingredients);
        bool TryCraft(List<RecipeIngredient> ingredients);
    }
}