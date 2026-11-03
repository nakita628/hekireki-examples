defmodule Calcom.InstantMeetingToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          token: String.t(),
          expires: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          booking: Calcom.Booking.t() | nil
        }

  schema "InstantMeetingToken" do
    field(:token, :string)
    field(:expires, Calcom.PrismaDateTime)
    field(:team_id, :id, source: :teamId)
    field(:booking_id, :id, source: :bookingId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end