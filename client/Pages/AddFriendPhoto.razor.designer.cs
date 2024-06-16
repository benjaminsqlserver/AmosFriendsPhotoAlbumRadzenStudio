using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using AmosFriendsPhotoAlbum.Models.ConData;
using AmosFriendsPhotoAlbum.Client.Pages;

namespace AmosFriendsPhotoAlbum.Pages
{
    public partial class AddFriendPhotoComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }

        AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto _friendphoto;
        protected AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto friendphoto
        {
            get
            {
                return _friendphoto;
            }
            set
            {
                if (!object.Equals(_friendphoto, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "friendphoto", NewValue = value, OldValue = _friendphoto };
                    _friendphoto = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Friend> _getFriendsForFriendIDResult;
        protected IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Friend> getFriendsForFriendIDResult
        {
            get
            {
                return _getFriendsForFriendIDResult;
            }
            set
            {
                if (!object.Equals(_getFriendsForFriendIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFriendsForFriendIDResult", NewValue = value, OldValue = _getFriendsForFriendIDResult };
                    _getFriendsForFriendIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getFriendsForFriendIDCount;
        protected int getFriendsForFriendIDCount
        {
            get
            {
                return _getFriendsForFriendIDCount;
            }
            set
            {
                if (!object.Equals(_getFriendsForFriendIDCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFriendsForFriendIDCount", NewValue = value, OldValue = _getFriendsForFriendIDCount };
                    _getFriendsForFriendIDCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            friendphoto = new AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto args)
        {
            try
            {
                var conDataCreateFriendPhotoResult = await ConData.CreateFriendPhoto(friendPhoto:friendphoto);
                DialogService.Close(friendphoto);
            }
            catch (System.Exception conDataCreateFriendPhotoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new FriendPhoto!" });
            }
        }

        protected async System.Threading.Tasks.Task FriendIdLoadData(LoadDataArgs args)
        {
            var conDataGetFriendsResult = await ConData.GetFriends(filter:$"contains(FirstName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getFriendsForFriendIDResult = conDataGetFriendsResult.Value.AsODataEnumerable();

            getFriendsForFriendIDCount = conDataGetFriendsResult.Count;
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
