public abstract class AbilityExecution
{
	public virtual bool CanExecute { get; protected set; }

	public abstract void OnInputStarted(AbilityController controller, Ability ability);
	public abstract void OnInputEnded(AbilityController controller, Ability ability);
}
