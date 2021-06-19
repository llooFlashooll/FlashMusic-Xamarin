﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashMusicApp.Interfaces;
using FlashMusicApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashMusicApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemQueryPage : ContentPage, ISearchPage
    {
        public ItemQueryPage()
        {
            InitializeComponent();
        }

        public void OnSearchBarTextChanged(string text)
        {
            try
            {
                var vm = this.BindingContext as ItemQueryViewModel;
                vm.Query(text);
            }
            catch (Exception ex)
            {
                DisplayAlert("错误提示", ex.Message, "取消");
            }
        }
    }
}