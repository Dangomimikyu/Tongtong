using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDictionary : IEqualityComparer<CommandAtrributes.Inputs[]>
{
    public bool Equals(CommandAtrributes.Inputs[] x, CommandAtrributes.Inputs[] y)
    {
        // compare lengths just in case
        if (x.Length != y.Length)
            return false;

        for (int i = 0; i < x.Length; ++i)
        {
            if (x[i] != y[i])
                return false;
        }
        return true;
    }

    public int GetHashCode(CommandAtrributes.Inputs[] inputs)
    {
        int hash = 13;
        for (int i = 0; i < inputs.Length; ++i)
        {
            unchecked
            {
                hash = (hash * 5) + (int)inputs[i];
            }
        }
        return hash;
    }
}
