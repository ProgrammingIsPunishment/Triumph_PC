using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskType TaskType { get; set; }
    public string Parameter { get; set; }

    public Task(TaskType taskType, string parameter)
    {
        this.TaskType = taskType;
        this.Parameter = parameter;
    }
}
