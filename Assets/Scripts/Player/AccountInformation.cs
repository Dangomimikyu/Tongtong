using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountInformation
{
    private uint money;
	// crafting material list

	public void ReceiveRewards(Quest q)
	{
		money += q.questRewards.money;
	}
}
