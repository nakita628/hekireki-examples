defmodule Calcom.BookingReport do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          booker_email: String.t(),
          reason: atom(),
          description: String.t() | nil,
          cancelled: boolean(),
          status: atom(),
          system_status: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          booking: Calcom.Booking.t() | nil,
          reported_by: Calcom.User.t() | nil,
          organization: Calcom.Team.t() | nil,
          watchlist: Calcom.Watchlist.t() | nil,
          global_watchlist: Calcom.Watchlist.t() | nil
        }

  schema "BookingReport" do
    field(:booker_email, :string, source: :bookerEmail)
    field(:reason, Ecto.Enum, values: [SPAM: "SPAM", DONT_KNOW_PERSON: "dont_know_person", OTHER: "OTHER"])
    field(:description, :string)
    field(:cancelled, :boolean, default: false)
    field(:status, Ecto.Enum, values: [:PENDING, :DISMISSED, :BLOCKED], default: :PENDING)
    field(:system_status, Ecto.Enum, values: [:PENDING, :BLOCKED, :DISMISSED], default: :PENDING, source: :systemStatus)
    field(:booking_uid, :string, source: :bookingUid)
    field(:reported_by_id, :id, source: :reportedById)
    field(:organization_id, :id, source: :organizationId)
    field(:watchlist_id, :binary_id, source: :watchlistId)
    field(:global_watchlist_id, :binary_id, source: :globalWatchlistId)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_uid, define_field: false, type: :string, references: :uid)
    belongs_to(:reported_by, Calcom.User, foreign_key: :reported_by_id, define_field: false, type: :id)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false, type: :id)
    belongs_to(:watchlist, Calcom.Watchlist, foreign_key: :watchlist_id, define_field: false)
    belongs_to(:global_watchlist, Calcom.Watchlist, foreign_key: :global_watchlist_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end