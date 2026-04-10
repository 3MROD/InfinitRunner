using System;

public static class EventSystem
{
    public static Action<bool> OnPlayerSlideDown;
    public static Action OnPlayerCollision;
    public static Action OnLucioleCollision;
    public static Action OnShipCollision;
    public static Action<int> OnPlayerLifeUpdate;
    public static Action<int> OnShipLifeUpdate;
    public static Action<int> LucioleUpdate;
    public static Action<bool> MegaCharge;
    public static Action<bool> MegaChargeReady;
    public static Action Flash;
    public static Action<ShipState> OnShipStateChange;
    public static Action FreeCow;
    public static Action<State> OnStateChanged;
}