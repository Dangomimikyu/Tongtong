using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAtrributes
{
	// inputs are the primitive actions done by the player
	public enum Inputs
	{
		None,
		Walk,
		Attack,
		Defend,
		Magic,
		test
	}

	// commands are the combination of inputs in a specific order and on-beat
	public enum Commands
	{
		Forward,            // W-W-W-A
		Retreat,            // W-W-W-D
		Attack,				// A-A-W-A
		DefendPhysical,     // D-A-D-A
		DefendMagical,		// D-M-M-D
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

	public enum ElementalMagicType
	{
		Earth,
		Ice,
		Fire,
	}
}
