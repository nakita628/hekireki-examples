defmodule Calcom.WatchlistEventAudit do
  use Ecto.Schema

  @primary_key {:id, Ecto.UUID, autogenerate: [version: 7]}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          watchlist_id: String.t(),
          event_type_id: integer(),
          action_taken: atom(),
          timestamp: DateTime.t()
        }

  schema "WatchlistEventAudit" do
    field(:watchlist_id, :string, source: :watchlistId)
    field(:event_type_id, :integer, source: :eventTypeId)
    field(:action_taken, Ecto.Enum, values: [:REPORT, :BLOCK, :ALERT], source: :actionTaken)
    field(:timestamp, Calcom.PrismaDateTime, autogenerate: true)
  end
end