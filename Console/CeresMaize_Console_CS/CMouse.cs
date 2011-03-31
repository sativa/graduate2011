using System.Collections;

namespace CeresMaize_Console_CS
{
    public struct CMouse
    {
        CMouseState state;
        object obj;
    };
    public enum CMouseState
    {
        None,
        GameInfo,
        Assart,
        Seminate,
        Reap,
        Irrigation,
        Fertilizer,
        Weed,
        Pet,
        Disease

    };

}