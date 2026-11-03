defmodule Calcom.WrongAssignmentReport do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          correct_assignee: String.t() | nil,
          additional_notes: String.t(),
          status: atom(),
          reviewed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          booking: Calcom.Booking.t() | nil,
          reported_by: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          reviewed_by: Calcom.User.t() | nil
        }

  schema "WrongAssignmentReport" do
    field(:correct_assignee, :string, source: :correctAssignee)
    field(:additional_notes, :string, source: :additionalNotes)
    field(:status, Ecto.Enum, values: [:PENDING, :REVIEWED, :RESOLVED, :DISMISSED], default: :PENDING)
    field(:reviewed_at, Calcom.PrismaDateTime, source: :reviewedAt)
    field(:booking_uid, :string, source: :bookingUid)
    field(:reported_by_id, :id, source: :reportedById)
    field(:team_id, :id, source: :teamId)
    field(:reviewed_by_id, :id, source: :reviewedById)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_uid, define_field: false, type: :string, references: :uid)
    belongs_to(:reported_by, Calcom.User, foreign_key: :reported_by_id, define_field: false, type: :id)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    belongs_to(:reviewed_by, Calcom.User, foreign_key: :reviewed_by_id, define_field: false, type: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end