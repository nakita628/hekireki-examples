defmodule Calcom.InternalNotePreset do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          cancellation_reason: String.t() | nil,
          inserted_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          booking_internal_note: [Calcom.BookingInternalNote.t()]
        }

  schema "InternalNotePreset" do
    field(:name, :string)
    field(:cancellation_reason, :string, source: :cancellationReason)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    has_many(:booking_internal_note, Calcom.BookingInternalNote, foreign_key: :note_preset_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end