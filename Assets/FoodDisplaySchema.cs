using System.Collections.Generic;

public class FoodDisplaySchema
{
    public bool requiresSelection;

    public List<Food> foods = new List<Food>();
    public DisplayType displayType;
    public enum  DisplayType
    {
        CheckingFood,
        AddingFood,
        CheckingMeal,
        AddingMeal
    }
    

    //Selecting 
    // 
    //
    
    
    //ADDING 
    // manual - provide texture and 100g constructor
    // auto = provide food itself
    
    //CHECKING   
    // food - tbd
    // meal - tbd
}