defmodule Calcom.WatchlistAudit do
  use Ecto.Schema

  @primary_key {:id, Ecto.UUID, autogenerate: [version: 7]}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          type: atom(),
          value: String.t(),
          description: String.t() | nil,
          action: atom(),
          changed_at: DateTime.t(),
          changed_by_user_id: integer() | nil,
          watchlist: Calcom.Watchlist.t() | nil
        }

  schema "WatchlistAudit" do
    field(:type, Ecto.Enum, values: [:EMAIL, :DOMAIN, :USERNAME])
    field(:value, :string)
    field(:description, :string)
    field(:action, Ecto.Enum, values: [:REPORT, :BLOCK, :ALERT], default: :REPORT)
    field(:changed_at, Calcom.PrismaDateTime, autogenerate: true, source: :changedAt)
    field(:changed_by_user_id, :integer, source: :changedByUserId)
    field(:watchlist_id, :binary_id, source: :watchlistId)
    belongs_to(:watchlist, Calcom.Watchlist, foreign_key: :watchlist_id, define_field: false)
  end
end