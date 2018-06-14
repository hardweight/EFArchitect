using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.Users
{
    public class UserListModel: PagedDataModel<UserDataModel>
    {

    }
    public class UserDataModel 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
