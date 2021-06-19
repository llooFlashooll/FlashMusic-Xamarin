using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using FlashMusicApp.Interfaces;
using FlashMusicApp.Service;

namespace FlashMusicApp.Core
{
    /// <summary>
    /// 容器，注入服务映射
    /// </summary>
    public class AutofacLocator : IAutofacLocator
    {
        IContainer container;

        public TInterface Get<TInterface>()
        {
            return container.Resolve<TInterface>();
        }

        /// <summary>
        /// 注册容器映射关系
        /// </summary>
        public void Register()
        {
            var Container = new ContainerBuilder();

            // 注入ToDo数据层服务映射
            Container.RegisterType<ToDoService>().As<IToDoService>();

            container = Container.Build();
        }
    }

    /// <summary>
    /// 全局容器管理器
    /// </summary>
    public class ServiceProvider
    {
        public static IAutofacLocator Instance { get; private set; }

        public static void RegisterSerivceLocator(IAutofacLocator locator)
        {
            Instance = locator;
        }
    }
}
