defmodule Rallly.RescheduledEventDate do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          start: DateTime.t(),
          end: DateTime.t(),
          all_day: boolean(),
          inserted_at: DateTime.t(),
          scheduled_event: Rallly.ScheduledEvent.t() | nil
        }

  schema "rescheduled_event_dates" do
    field(:id, :string, primary_key: true)
    field(:start, Rallly.PrismaDateTime)
    field(:end, Rallly.PrismaDateTime)
    field(:all_day, :boolean, default: false)
    belongs_to(:scheduled_event, Rallly.ScheduledEvent, foreign_key: :scheduled_event_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end