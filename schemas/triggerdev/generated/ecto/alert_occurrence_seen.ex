defmodule Trigger.AlertOccurrenceSeen do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          alert_id: String.t(),
          source_event_id: String.t(),
          row_time: DateTime.t(),
          seen_at: DateTime.t(),
          expires_at: DateTime.t()
        }

  schema "AlertOccurrenceSeen" do
    field(:alert_id, :string, primary_key: true, source: :alertId)
    field(:source_event_id, :string, primary_key: true, source: :sourceEventId)
    field(:row_time, Trigger.PrismaDateTime, source: :rowTime)
    field(:seen_at, Trigger.PrismaDateTime, autogenerate: true, source: :seenAt)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
  end
end