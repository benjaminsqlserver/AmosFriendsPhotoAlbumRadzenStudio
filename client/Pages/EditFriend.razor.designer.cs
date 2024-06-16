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
    public partial class EditFriendComponent : ComponentBase
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

        [Parameter]
        public dynamic FriendID { get; set; }

        bool _hasChanges;
        protected bool hasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (!object.Equals(_hasChanges, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "hasChanges", NewValue = value, OldValue = _hasChanges };
                    _hasChanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (!object.Equals(_canEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "canEdit", NewValue = value, OldValue = _canEdit };
                    _canEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        AmosFriendsPhotoAlbum.Models.ConData.Friend _friend;
        protected AmosFriendsPhotoAlbum.Models.ConData.Friend friend
        {
            get
            {
                return _friend;
            }
            set
            {
                if (!object.Equals(_friend, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "friend", NewValue = value, OldValue = _friend };
                    _friend = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        AmosFriendsPhotoAlbum.Models.ConData.Gender _getByGendersForGenderIDResult;
        protected AmosFriendsPhotoAlbum.Models.ConData.Gender getByGendersForGenderIDResult
        {
            get
            {
                return _getByGendersForGenderIDResult;
            }
            set
            {
                if (!object.Equals(_getByGendersForGenderIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByGendersForGenderIDResult", NewValue = value, OldValue = _getByGendersForGenderIDResult };
                    _getByGendersForGenderIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Gender> _getGendersForGenderIDResult;
        protected IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Gender> getGendersForGenderIDResult
        {
            get
            {
                return _getGendersForGenderIDResult;
            }
            set
            {
                if (!object.Equals(_getGendersForGenderIDResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGendersForGenderIDResult", NewValue = value, OldValue = _getGendersForGenderIDResult };
                    _getGendersForGenderIDResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getGendersForGenderIDCount;
        protected int getGendersForGenderIDCount
        {
            get
            {
                return _getGendersForGenderIDCount;
            }
            set
            {
                if (!object.Equals(_getGendersForGenderIDCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGendersForGenderIDCount", NewValue = value, OldValue = _getGendersForGenderIDCount };
                    _getGendersForGenderIDCount = value;
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
            hasChanges = false;

            canEdit = true;

            var conDataGetFriendByFriendIdResult = await ConData.GetFriendByFriendId(friendId:FriendID);
            friend = conDataGetFriendByFriendIdResult;

            canEdit = conDataGetFriendByFriendIdResult != null;

            if (this.friend.GenderID != null)
            {
                var conDataGetGenderByGenderIdResult = await ConData.GetGenderByGenderId(genderId:this.friend.GenderID);
                getByGendersForGenderIDResult = conDataGetGenderByGenderIdResult;
            }
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(AmosFriendsPhotoAlbum.Models.ConData.Friend args)
        {
            try
            {
                var conDataUpdateFriendResult = await ConData.UpdateFriend(friendId:FriendID, friend:friend);
                if (conDataUpdateFriendResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(friend);
                }

                hasChanges = conDataUpdateFriendResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception conDataUpdateFriendException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Friend" });

            hasChanges = conDataUpdateFriendException.Message.Contains("412");

            if (!conDataUpdateFriendException.Message.Contains("412")) {
                canEdit = false;
            }
            }
        }

        protected async System.Threading.Tasks.Task GenderIdLoadData(LoadDataArgs args)
        {
            var conDataGetGendersResult = await ConData.GetGenders(filter:$"contains(GenderName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getGendersForGenderIDResult = conDataGetGendersResult.Value.AsODataEnumerable();

            getGendersForGenderIDCount = conDataGetGendersResult.Count;
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
