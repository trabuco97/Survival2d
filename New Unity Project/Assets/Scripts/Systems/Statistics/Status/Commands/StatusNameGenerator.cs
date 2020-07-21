using UnityEngine;
using System.Collections;

using ConsoleChat;

namespace Survival2D.Systems.Statistics.Status.Command
{
    public class StatusNameGenerator : ITabNamesGenerator
    {
        protected override string[] GetNamesToCompare()
        {
            return StatusDatabase.Instance.GetStatusNames();
        }
    }
}