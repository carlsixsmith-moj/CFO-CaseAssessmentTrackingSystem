using Cfo.Cats.Application.Features.Locations.DTOs;
using Cfo.Cats.Application.Features.Participants.DTOs;
using Cfo.Cats.Application.Features.Participants.Queries;
using Cfo.Cats.Application.Features.Participants.Specifications;

namespace Cfo.Cats.Server.UI.Pages.Participants;

public partial class ParticipantsV2
{
    private LocationDto[] _locations = [];
    private ParticipantListView _listView = ParticipantListView.Default;
    private string _orderBy = "Id";
    private string _orderDirection = "asc";
    private MudTable<ParticipantPaginationDto>? _table;

    protected override IRequest<Result<PaginatedData<ParticipantPaginationDto>>> CreateQuery()
        => new ParticipantsWithPagination.Query()
        {
            CurrentUser = CurrentUser,
            JustMyCases = false,
            ListView = _listView,
            PageNumber = 1,
            PageSize = 10,
            Locations = _locations.Select(x => x.Id).ToArray(),
            OrderBy = _orderBy,
            SortDirection = _orderDirection
        };
    private async Task AddLocationFilter(LocationDto location)
    {
        if (_locations.Contains(location))
        {
            return;
        }

        _locations = [.._locations, location];
        await LoadDataAsync();
    }

    private async Task RemoveLocation(LocationDto location)
    {
        if (!_locations.Contains(location))
        {
            return;
        }

        _locations = _locations.Except([location]).ToArray();
        await LoadDataAsync();
    }

    private async Task OnChangedListView(ParticipantListView arg)
    {
        if (arg == _listView)
        {
            return;
        }
        _listView = arg;
        await LoadDataAsync();
    }

    private async Task<TableData<ParticipantPaginationDto>> ServerReload(TableState state, CancellationToken cancellationToken)
    {
        _orderBy = state.SortLabel ?? "Id";
        _orderDirection = state.SortDirection.ToString();
        if (await LoadDataAsync(cancellationToken))
        {
            return new TableData<ParticipantPaginationDto>()
            {
                Items = Data!.Items,
                TotalItems = Data!.TotalItems,
            };
        }

        return new TableData<ParticipantPaginationDto>();
    }
}