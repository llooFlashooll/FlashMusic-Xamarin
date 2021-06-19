using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using FlashMusicApp.Module;

namespace FlashMusicApp.Core
{
    public class Constants
    {
        /// <summary>
        /// 本地数据库脚本路径
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ToDo.db");
            }
        }

        public static async void InitAsync(ToDoContext context)
        {
            try
            {
                // 初始化数据库
                context.Database.EnsureCreated();
                if (!context.Checklists.Any())
                {
                    await context.Checklists.AddRangeAsync(new Checklist[]
                    {
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe635", Title = "我的一天", BackColor = "#218868", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe6b6", Title = "重要", BackColor = "#ee7800", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe6e1", Title = "已计划日程", BackColor = "#884898", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe614", Title = "音乐学习", BackColor = "#e83929", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe755", Title = "歌词创作", BackColor = "#fabf14", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe63b", Title = "购物清单", BackColor = "#0094c8", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe63b", Title = "待上课程", BackColor = "#d4acad", },
                   new Checklist() { Id=Guid.NewGuid().ToString(), IconFont = "\xe63b", Title = "待办事项", BackColor = "#839b5c", },
                });
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
