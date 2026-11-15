using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;

    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing) return;
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState)
    {
        currentState = nextState;
    }
}
