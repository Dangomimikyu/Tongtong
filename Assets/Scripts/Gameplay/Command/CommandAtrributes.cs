public class CommandAtrributes
{
	// inputs are the primitive actions done by the player
	public enum Inputs
	{
		None,
		BeatEnd,
		Walk,
		Attack,
		Defend,
		Magic
	}

	// commands are the combination of inputs in a specific order and on-beat
	public enum Commands
	{
		Forward,            // W-W-W-A
		Retreat,            // W-W-W-D
		AttackStraight,		// A-A-W-A
		AttackUpward,		// A-W-A-A
		Defend,				// D-A-D-A
		Focus,				// D-D-M-A
		Pray,				// M-M-M-D, M-D-A-M (takes 2 turns for miracle to work)
		Total
	}

	// potency is dependent on the accuracy of the player's timing; lowest potency will be taken during the processing
	public enum Potency
	{
		Low,
		Medium,
		High
	}
}
