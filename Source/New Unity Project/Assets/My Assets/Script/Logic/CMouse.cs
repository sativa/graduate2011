using System.Collections;

public class CMouse
{
    public CMouseState state;
    public object obj;

    public CMouse()
    {
        state = CMouseState.None;
        obj = null;
    }

};
public enum CMouseState
{
    None,
    Assart,
    Seminate,
    Reap,
    Irrigation,
    Fertilizer,
    Weed,
    Pet,
    Disease
};
