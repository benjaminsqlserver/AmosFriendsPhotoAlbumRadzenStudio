using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AmosFriendsPhotoAlbum.Models;
using AmosFriendsPhotoAlbum.Models.ConData;
using Radzen;

namespace AmosFriendsPhotoAlbum
{
    public partial class GlobalsService
    {

    }

    public class PropertyChangedEventArgs
    {
        public string Name { get; set; }
        public object NewValue { get; set; }
        public object OldValue { get; set; }
        public bool IsGlobal { get; set; }
    }
}
