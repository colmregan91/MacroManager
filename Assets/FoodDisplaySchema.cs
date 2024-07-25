public class FoodDisplaySchema
{
    public bool requiresSelection;

    public Food food;
    public DisplayType displayType;
    public enum  DisplayType
    {
        Manual,
        auto
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