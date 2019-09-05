using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model
{
  class ContactListsModel
  {
    public string ListName { get; set; }
    public string ImagePath { get; set; }
    public List<DisplayedContactModel> ContactsList { get; set; }
  }
}
