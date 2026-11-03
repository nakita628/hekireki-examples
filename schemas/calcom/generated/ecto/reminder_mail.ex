defmodule Calcom.ReminderMail do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          reference_id: integer(),
          reminder_type: atom(),
          elapsed_minutes: integer(),
          inserted_at: DateTime.t()
        }

  schema "ReminderMail" do
    field(:reference_id, :integer, source: :referenceId)
    field(:reminder_type, Ecto.Enum, values: [:PENDING_BOOKING_CONFIRMATION], source: :reminderType)
    field(:elapsed_minutes, :integer, source: :elapsedMinutes)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end