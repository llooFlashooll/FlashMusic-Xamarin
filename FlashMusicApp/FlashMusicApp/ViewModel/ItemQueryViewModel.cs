﻿using System;
using System.Collections.Generic;
using System.Text;
using FlashMusicApp.Core;
using FlashMusicApp.Interfaces;
using FlashMusicApp.Module;
using System.Linq;

namespace FlashMusicApp.ViewModel
{
    public class ItemQueryViewModel : ItemDetailViewModel
    {
        private readonly IToDoService toDoService;

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="checklist"></param>
        public ItemQueryViewModel(SingleChecklist checklist) : base(checklist)
        {
            toDoService = ServiceProvider.Instance.Get<IToDoService>();
        }

        public async void Query(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                SingleChecklist.ChecklistDetails = new System.Collections.ObjectModel.ObservableCollection<ChecklistDetail>();
            }
            else
            {
                var cks = await toDoService.GetToDoListDetailByTextAsync(content);
                if (cks != null)
                {
                    SingleChecklist.ChecklistDetails = new System.Collections.ObjectModel.ObservableCollection<ChecklistDetail>();
                    cks.ForEach(arg =>
                    {
                        SingleChecklist.ChecklistDetails.Add(arg);
                    });
                }
            }
        }
    }
}
