defmodule Rallly.SheetSlot do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          start_time: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          sheet: Rallly.Sheet.t() | nil,
          event_type: Rallly.EventType.t() | nil,
          scheduled_event: Rallly.ScheduledEvent.t() | nil
        }

  schema "sheet_slots" do
    field(:id, :string, primary_key: true)
    field(:start_time, Rallly.PrismaDateTime)
    belongs_to(:sheet, Rallly.Sheet, foreign_key: :sheet_id, type: :string)
    belongs_to(:event_type, Rallly.EventType, foreign_key: :event_type_id, type: :string)
    has_one(:scheduled_event, Rallly.ScheduledEvent, foreign_key: :sheet_slot_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end