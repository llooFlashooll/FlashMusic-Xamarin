using System;
using System.Collections.Generic;
using System.Text;

namespace FlashMusicApp.Core
{
    public interface IAutofacLocator
    {
        void Register();

        TInterface Get<TInterface>();
    }
}
