using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountInformation : MonoBehaviour
{
    public int money;
	[SerializeField]
	private int m_level = 1;
	[SerializeField]
	private float m_exp;
	[SerializeField]
	private float m_neededExp;

	#region Getter functions
	public int GetPlayerLevel()
    {
		return m_level;
    }

	public float GetCurrentExp()
	{
		return m_exp;
	}

	public float GetNeededExp()
	{
		return m_neededExp;
	}
	#endregion

	#region Save file functions
	public void UpdateInfo(FileSaveManager.SaveObject so)
	{
		money = so.playerMoney;
		m_level = so.playerLevel;
		m_exp = so.currentExp;
		m_neededExp = so.neededExp;
	}
	#endregion

	#region Quest functions
	public void ReceiveRewards(Quest q)
	{
		Debug.Log("quest reward received: " + q.questRewards.money);
		money += q.questRewards.money;

		CheckLevelUp(q.questRewards.exp);
    }

	private void CheckLevelUp(int newExp)
    {
		m_exp += newExp;
		while(m_exp >= m_neededExp)
        {
			m_exp -= m_neededExp;
			m_level++;
			UpdateNeededExp();
        }
    }

	private void UpdateNeededExp()
    {
		m_neededExp *= 1.5f * (m_neededExp); // temp [to remove] or more accurately to change but im too tired rn man
    }
    #endregion
}
