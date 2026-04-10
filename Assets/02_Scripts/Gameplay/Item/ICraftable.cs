
using Elemental.Gameplay.item;
using System.Collections.Generic;

public interface ICraftable
{
    bool CanCraft(List<RecipeIngredient> ingredients);
    bool TryCraft(List<RecipeIngredient> ingredients);
}
