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
    public partial class AddFriendComponent : ComponentBase
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
            friend = new AmosFriendsPhotoAlbum.Models.ConData.Friend(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(AmosFriendsPhotoAlbum.Models.ConData.Friend args)
        {
            try
            {
                var conDataCreateFriendResult = await ConData.CreateFriend(friend:friend);
                DialogService.Close(friend);
            }
            catch (System.Exception conDataCreateFriendException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Friend!" });
            }
        }

        protected async System.Threading.Tasks.Task GenderIdLoadData(LoadDataArgs args)
        {
            var conDataGetGendersResult = await ConData.GetGenders(filter:$"contains(GenderName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getGendersForGenderIDResult = conDataGetGendersResult.Value.AsODataEnumerable();

            getGendersForGenderIDCount = conDataGetGendersResult.Count;
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
