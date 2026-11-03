defmodule Calcom.HashedLink do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          link: String.t(),
          expires_at: DateTime.t() | nil,
          max_usage_count: integer(),
          usage_count: integer(),
          event_type: Calcom.EventType.t() | nil
        }

  schema "HashedLink" do
    field(:link, :string)
    field(:expires_at, Calcom.PrismaDateTime, source: :expiresAt)
    field(:max_usage_count, :integer, default: 1, source: :maxUsageCount)
    field(:usage_count, :integer, default: 0, source: :usageCount)
    field(:event_type_id, :id, source: :eventTypeId)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
  end
end