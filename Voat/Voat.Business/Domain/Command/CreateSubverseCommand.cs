﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voat.Caching;
using Voat.Data;

namespace Voat.Domain.Command
{
    public class CreateSubverseCommand : CacheCommand<CommandResponse>
    {
        private string _name;
        private string _title;
        private string _sidebar;
        private string _description;

        public CreateSubverseCommand(string name, string title, string description, string sidebar = null)
        {
            this._name = name;
            this._title = title;
            this._sidebar = sidebar;
            this._description = description;
        }

        //protected override async Task<CommandResponse> ProtectedExecute()
        //{
           
        //}

        protected override async Task<CommandResponse> CacheExecute()
        {
            using (var repo = new Repository())
            {
                return await repo.CreateSubverse(_name, _title, _description, _sidebar);
            }
        }

        protected override void UpdateCache(CommandResponse result)
        {
            if (result.Success)
            {
                CacheHandler.Instance.Remove(CachingKey.UserSubscriptions(UserName));
            }
        }
    }
}
