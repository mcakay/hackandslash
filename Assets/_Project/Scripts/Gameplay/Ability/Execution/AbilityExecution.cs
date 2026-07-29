public abstract class AbilityExecution
{
	public virtual bool CanExecute { get; protected set; }

	public abstract void OnInputStarted(AbilityRunner runner, AbilitySO ability);
	public abstract void OnInputEnded(AbilityRunner runner, AbilitySO ability);
}
