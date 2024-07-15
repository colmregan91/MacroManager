using System;

public class DropdownAdder : DropdownDisplay
{
    public Action<Food> OnFoodAdded;
    public override void HandleDropdownSelection(int selection)
    {
        var food = DisplayedFoods[selection];
        OnFoodAdded?.Invoke(food);
    }
}