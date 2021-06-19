using System;
using FlashMusicApp.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashMusicApp
{
    /// <summary>
    /// 用于关联 Sqlite数据库
    /// </summary>
    public partial class App : Application
    {
        private static ToDoContext database;

        public static ToDoContext Instance
        {
            get
            {
                if (database == null)
                {
                    database = new ToDoContext(Constants.DatabasePath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            Constants.InitAsync(Instance);
            // 初始化容器，用于注册服务
            AutofacLocator autofac = new AutofacLocator();
            autofac.Register();
            ServiceProvider.RegisterSerivceLocator(autofac);
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
