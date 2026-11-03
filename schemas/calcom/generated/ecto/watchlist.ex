defmodule Calcom.Watchlist do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          type: atom(),
          value: String.t(),
          description: String.t() | nil,
          is_global: boolean(),
          organization_id: integer() | nil,
          action: atom(),
          source: atom(),
          last_updated_at: DateTime.t(),
          booking_reports: [Calcom.BookingReport.t()],
          global_booking_reports: [Calcom.BookingReport.t()],
          audits: [Calcom.WatchlistAudit.t()]
        }

  schema "Watchlist" do
    field(:type, Ecto.Enum, values: [:EMAIL, :DOMAIN, :USERNAME])
    field(:value, :string)
    field(:description, :string)
    field(:is_global, :boolean, default: false, source: :isGlobal)
    field(:organization_id, :integer, source: :organizationId)
    field(:action, Ecto.Enum, values: [:REPORT, :BLOCK, :ALERT], default: :REPORT)
    field(:source, Ecto.Enum, values: [:MANUAL, :FREE_DOMAIN_POLICY, :SIGNUP], default: :MANUAL)
    field(:last_updated_at, Calcom.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    has_many(:booking_reports, Calcom.BookingReport, foreign_key: :watchlist_id)
    has_many(:global_booking_reports, Calcom.BookingReport, foreign_key: :global_watchlist_id)
    has_many(:audits, Calcom.WatchlistAudit, foreign_key: :watchlist_id)
  end
end