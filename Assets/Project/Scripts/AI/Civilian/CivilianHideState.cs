public class CivilianHideState : State
{
    public string defaultHideName = "Hide";

    public override State RunCurrentState()
    {
        PlayAnimation(defaultHideName);
        return this;
    }
}