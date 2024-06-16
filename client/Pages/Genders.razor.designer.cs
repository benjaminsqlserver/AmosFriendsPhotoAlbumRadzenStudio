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
    public partial class GendersComponent : ComponentBase
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
        protected RadzenDataGrid<AmosFriendsPhotoAlbum.Models.ConData.Gender> grid0;

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

        IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Gender> _getGendersResult;
        protected IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.Gender> getGendersResult
        {
            get
            {
                return _getGendersResult;
            }
            set
            {
                if (!object.Equals(_getGendersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGendersResult", NewValue = value, OldValue = _getGendersResult };
                    _getGendersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getGendersCount;
        protected int getGendersCount
        {
            get
            {
                return _getGendersCount;
            }
            set
            {
                if (!object.Equals(_getGendersCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getGendersCount", NewValue = value, OldValue = _getGendersCount };
                    _getGendersCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddGender>("Add Gender", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ConData.ExportGendersToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "GenderID,GenderName" }, $"Genders");

            }

            if (args == null || args.Value == "xlsx")
            {
                await ConData.ExportGendersToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "GenderID,GenderName" }, $"Genders");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var conDataGetGendersResult = await ConData.GetGenders(filter:$@"(contains(GenderName,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getGendersResult = conDataGetGendersResult.Value.AsODataEnumerable();

                getGendersCount = conDataGetGendersResult.Count;
            }
            catch (System.Exception conDataGetGendersException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Genders" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<AmosFriendsPhotoAlbum.Models.ConData.Gender> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditGender>("Edit Gender", new Dictionary<string, object>() { {"GenderID", args.Data.GenderID} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var conDataDeleteGenderResult = await ConData.DeleteGender(genderId:data.GenderID);
                    if (conDataDeleteGenderResult != null && conDataDeleteGenderResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (conDataDeleteGenderResult != null && conDataDeleteGenderResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Gender" });
                    }
                }
            }
            catch (System.Exception conDataDeleteGenderException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Gender" });
            }
        }
    }
}
