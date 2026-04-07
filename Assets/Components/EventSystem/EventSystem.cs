using System;

public static class EventSystem
{
    public static Action<bool> OnPlayerSlideDown;
    public static Action OnPlayerCollision;
    public static Action<int> OnPlayerLifeUpdate;
    public static Action<ShipState> OnShipStateChange;
    
    public static Action<State> OnStateChanged;
}