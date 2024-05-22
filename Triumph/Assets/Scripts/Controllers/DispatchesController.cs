using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatchesController : MonoBehaviour
{
    public void Process(Dispatch dispatch)
    {
        string wokringMessage = dispatch.Message.ToUpper();

        string[] distinctCommands = wokringMessage.Split('.');
        List<string[]> commands = new List<string[]>();

        foreach (string dc in distinctCommands)
        {
            commands.Add(wokringMessage.Split(' '));
        }

        List<Task> workingTasks = new List<Task>();

        foreach (string[] s in commands)
        {
            //holding workingholding = null;

            switch (s[0])
            {
                case "MOVE":
                    switch (s[1])
                    {
                        case "TO":
                            //workingHolding = Oberkommando.SAVE.Holdings.Find(h=>h.Name.ToUpper() == s[2]);
                            workingTasks.Add(new Task(TaskType.Move, s[2]));
                            break;
                    }
                    break;
            }
        }
    }
}
