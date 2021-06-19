using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FlashMusicApp.Interfaces;
using FlashMusicApp.Module;
using System.Linq;
using FlashMusicApp.Core.Helper;

namespace FlashMusicApp.Service
{
    /// <summary>
    /// 定义service服务，用于进行数据操作
    /// 使用 Linq 操作
    /// </summary>
    public class ToDoService : IToDoService
    {
        /// <summary>
        /// 获取首页数据列表
        /// </summary>
        public async Task<List<Checklist>> GetToDoListAsync()
        {
            try
            {
                var cks = App.Instance.Checklists.ToList();
                cks.ForEach(arg =>
                {
                    arg.Count = App.Instance.ChecklistDetails.Where(t => t.ChecklistId == arg.Id && t.IsDeleted == false).Count();
                });
                return cks;
            }
            catch (Exception ex)
            {
                return new List<Checklist>();
            }
        }

        /// <summary>
        /// 获取清单明细的数据列表
        /// </summary>
        public async Task<SingleChecklist> GetToDoListDetailAsync(string id)
        {
            try
            {
                var ck = App.Instance.Checklists.FirstOrDefault(t => t.Id == id);
                var cks = App.Instance.ChecklistDetails.Where(t => t.ChecklistId == id).ToList();
                return new SingleChecklist()
                {
                    Checklist = ck,
                    ChecklistDetails = cks == null ?
                    new System.Collections.ObjectModel.ObservableCollection<ChecklistDetail>() :
                    cks.ToObservableCollection()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据内容搜索结果
        /// </summary>
        public async Task<List<ChecklistDetail>> GetToDoListDetailByTextAsync(string text)
        {
            try
            {
                return App.Instance.ChecklistDetails.Where(t => t.Content.Contains(text)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 首页添加新的清单
        /// </summary>
        public async Task<bool> AddToDoGroupAsync(Checklist checklist)
        {
            try
            {
                await App.Instance.Checklists.AddAsync(checklist);
                return await App.Instance.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除首页的清单
        /// </summary>
        public async Task<bool> DeleteToDoGroupByIdAsync(string id)
        {
            try
            {
                var ck = App.Instance.Checklists.FirstOrDefault(t => t.Id == id);
                if (ck != null)
                {
                    App.Instance.Checklists.Remove(ck);
                    return await App.Instance.SaveChangesAsync() > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除明细表当中的书
        /// </summary>
        public async Task<bool> DeleteToDoInfoByIdAsync(string id)
        {
            try
            {
                var ckd = App.Instance.ChecklistDetails.FirstOrDefault(t => t.Id == id);
                if (ckd != null)
                {
                    App.Instance.ChecklistDetails.Remove(ckd);
                    return await App.Instance.SaveChangesAsync() > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新首页清单的名称
        /// </summary>
        public async Task<bool> UpdateToDoGroupNameAsync(string id, string name)
        {
            try
            {
                var ckd = App.Instance.Checklists.FirstOrDefault(t => t.Id == id);
                if (ckd != null)
                {
                    ckd.Title = name;
                    App.Instance.Entry(ckd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    return await App.Instance.SaveChangesAsync() > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 添加明细
        /// </summary>
        public async Task<bool> AddToDoDetailAsync(string id, ChecklistDetail detail)
        {
            try
            {
                var cl = App.Instance.Checklists.FirstOrDefault(t => t.Id == id);
                if (cl != null)
                {
                    detail.ChecklistId = cl.Id;
                    await App.Instance.ChecklistDetails.AddAsync(detail);
                    return await App.Instance.SaveChangesAsync() > 0 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public async Task<bool> UpdateDeleteStatus(string id, bool status)
        {
            try
            {
                var ckd = App.Instance.ChecklistDetails.FirstOrDefault(t => t.Id == id);
                if (ckd != null)
                {
                    ckd.IsDeleted = status;
                    App.Instance.Entry(ckd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    return await App.Instance.SaveChangesAsync() > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public async Task<bool> UpdateFavoriteStatus(string id, bool status)
        {
            try
            {
                var ckd = App.Instance.ChecklistDetails.FirstOrDefault(t => t.Id == id);
                if (ckd != null)
                {
                    ckd.IsFavorite = status;
                    App.Instance.Entry(ckd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    return await App.Instance.SaveChangesAsync() > 0 ? true : false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
