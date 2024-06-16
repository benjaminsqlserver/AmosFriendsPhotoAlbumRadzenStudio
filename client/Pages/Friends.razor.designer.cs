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
    public partial class FriendsComponent : ComponentBase
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
        protected RadzenDataGrid<AmosFriendsPhotoAlbum.Models.ConData.Friend> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Friend> _getFriendsResult;
        protected IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Friend> getFriendsResult
        {
            get
            {
                return _getFriendsResult;
            }
            set
            {
                if (!object.Equals(_getFriendsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFriendsResult", NewValue = value, OldValue = _getFriendsResult };
                    _getFriendsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getFriendsCount;
        protected int getFriendsCount
        {
            get
            {
                return _getFriendsCount;
            }
            set
            {
                if (!object.Equals(_getFriendsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFriendsCount", NewValue = value, OldValue = _getFriendsCount };
                    _getFriendsCount = value;
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
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddFriend>("Add Friend", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ConData.ExportFriendsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Gender", Select = "FriendID,FirstName,LastName,DateOfBirth,Gender.GenderName as GenderGenderName,EmailAddress,PhoneNumber" }, $"Friends");

            }

            if (args == null || args.Value == "xlsx")
            {
                await ConData.ExportFriendsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Gender", Select = "FriendID,FirstName,LastName,DateOfBirth,Gender.GenderName as GenderGenderName,EmailAddress,PhoneNumber" }, $"Friends");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var conDataGetFriendsResult = await ConData.GetFriends(filter:$@"(contains(FirstName,""{search}"") or contains(LastName,""{search}"") or contains(EmailAddress,""{search}"") or contains(PhoneNumber,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Gender", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getFriendsResult = conDataGetFriendsResult.Value.AsODataEnumerable();

                getFriendsCount = conDataGetFriendsResult.Count;
            }
            catch (System.Exception conDataGetFriendsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Friends" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<AmosFriendsPhotoAlbum.Models.ConData.Friend> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditFriend>("Edit Friend", new Dictionary<string, object>() { {"FriendID", args.Data.FriendID} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var conDataDeleteFriendResult = await ConData.DeleteFriend(friendId:data.FriendID);
                    if (conDataDeleteFriendResult != null && conDataDeleteFriendResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (conDataDeleteFriendResult != null && conDataDeleteFriendResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Friend" });
                    }
                }
            }
            catch (System.Exception conDataDeleteFriendException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Friend" });
            }
        }
    }
}
