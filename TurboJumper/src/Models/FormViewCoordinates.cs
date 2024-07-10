namespace TurboJumper.Models;

public class FormViewCoordinates
{
    private int _x = 0;
    private int _y = 0;

    public FormViewCoordinates(int x, int y)
    {
        this._x = x;
        this._y = y;
    }
    
    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }
}