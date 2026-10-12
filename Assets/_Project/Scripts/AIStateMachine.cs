using UnityEngine;
using System.Collections.Generic;

public abstract class AIState
{
    protected AIStateMachine stateMachine;

    public AIState(AIStateMachine sm) => stateMachine = sm;

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}

public class AIStateMachine : MonoBehaviour
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>();
    private AIState currentState;
    private string currentStateName;

    public void RegisterState(string name, AIState state)
    {
        states[name] = state;
    }

    public void SetState(string stateName)
    {
        if (stateName == currentStateName) return;

        if (currentState != null)
            currentState.OnExit();

        if (states.TryGetValue(stateName, out AIState newState))
        {
            currentState = newState;
            currentStateName = stateName;
            currentState.OnEnter();
        }
        else
        {
            Debug.LogWarning($"Estado '{stateName}' no encontrado");
        }
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }

    public string GetCurrentStateName() => currentStateName;
    public AIState GetCurrentState() => currentState;
}
