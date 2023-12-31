using System.Collections.Generic;
using UnityEngine;

namespace Potions.Gameplay
{
    public class TaskList : MonoBehaviour
    {
        public void Show()
        {
            LeanTween.cancel(gameObject);
            transform.localScale = new Vector3(1f, 0f, 1f);
            LeanTween.scaleY(gameObject, 1f, 0.125f).setEaseOutCubic();
        }

        public void Hide()
        {
            LeanTween.cancel(gameObject);
            transform.localScale = new Vector3(1f, 1f, 1f);
            LeanTween.scaleY(gameObject, 0f, 0.125f).setEaseInCubic();
        }

        public void Setup(int numTasksPerRow, int numRows)
        {
            float width = numTasksPerRow * _taskWidth;
            float height = numRows * _taskHeight;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numTasksPerRow; j++)
                {
                    GameObject blipObject = Instantiate(blipPrefab);

                    blipObject.transform.SetParent(transform);
                    blipObject.transform.localPosition = new Vector2(-width / 2 + j * width / (numTasksPerRow - 1),
                        height / 2 - i * height / numRows);
                    blipObject.transform.localScale = new Vector3(1f, 1f, 1f);

                    _blips.Add(blipObject.GetComponent<TaskListBlip>());
                }
            }
        }

        public void SetStateLearning(List<Potions.Gameplay.GolemBrain.Task> tasks)
        {
            for (int i = 0; i < _blips.Count; i++)
            {
                if (tasks != null && i < tasks.Count)
                {
                    _blips[i].SetState(TaskListBlip.TASK_BLIP_STATE.IDLE, tasks[i].Type);
                }
                else
                {
                    _blips[i].SetState(TaskListBlip.TASK_BLIP_STATE.NONE);
                }
            }
        }

        public void SetStateExecuting(List<Potions.Gameplay.GolemBrain.Task> tasks, int executingIndex)
        {
            for (int i = 0; i < _blips.Count; i++)
            {
                if (i < tasks.Count)
                {
                    if (i < executingIndex)
                    {
                        _blips[i].SetState(TaskListBlip.TASK_BLIP_STATE.DONE, tasks[i].Type);
                    }
                    else if (i == executingIndex)
                    {
                        _blips[i].SetState(TaskListBlip.TASK_BLIP_STATE.DOING, tasks[i].Type);
                    }
                    else
                    {
                        _blips[i].SetState(TaskListBlip.TASK_BLIP_STATE.IDLE, tasks[i].Type);
                    }
                }
                else
                {
                    _blips[i].SetState(TaskListBlip.TASK_BLIP_STATE.NONE);
                }
            }
        }

        public GameObject blipPrefab;

        private float _taskWidth = 0.12f;
        private float _taskHeight = 0.12f;
        private List<TaskListBlip> _blips = new();
    }
}