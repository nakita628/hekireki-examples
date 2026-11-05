defmodule InboxZero.AvailabilityWindow do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          weekday: integer(),
          start_minutes: integer(),
          end_minutes: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          availability_schedule: InboxZero.AvailabilitySchedule.t() | nil
        }

  schema "AvailabilityWindow" do
    field(:id, :string, primary_key: true)
    field(:weekday, :integer)
    field(:start_minutes, :integer, source: :startMinutes)
    field(:end_minutes, :integer, source: :endMinutes)
    field(:availability_schedule_id, :string, source: :availabilityScheduleId)
    belongs_to(:availability_schedule, InboxZero.AvailabilitySchedule, foreign_key: :availability_schedule_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end