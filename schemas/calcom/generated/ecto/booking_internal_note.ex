defmodule Calcom.BookingInternalNote do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          text: String.t() | nil,
          inserted_at: DateTime.t(),
          note_preset: Calcom.InternalNotePreset.t() | nil,
          booking: Calcom.Booking.t() | nil,
          created_by: Calcom.User.t() | nil
        }

  schema "BookingInternalNote" do
    field(:text, :string)
    field(:note_preset_id, :id, source: :notePresetId)
    field(:booking_id, :id, source: :bookingId)
    field(:created_by_id, :id, source: :createdById)
    belongs_to(:note_preset, Calcom.InternalNotePreset, foreign_key: :note_preset_id, define_field: false)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
    belongs_to(:created_by, Calcom.User, foreign_key: :created_by_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end