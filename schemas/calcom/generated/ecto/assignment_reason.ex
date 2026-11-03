defmodule Calcom.AssignmentReason do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          reason_enum: atom(),
          reason_string: String.t(),
          inserted_at: DateTime.t(),
          booking: Calcom.Booking.t() | nil
        }

  schema "AssignmentReason" do
    field(:reason_enum, Ecto.Enum, values: [:REASSIGNED, :RR_REASSIGNED, :REROUTED, :SALESFORCE_ASSIGNMENT], source: :reasonEnum)
    field(:reason_string, :string, source: :reasonString)
    field(:booking_id, :id, source: :bookingId)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end