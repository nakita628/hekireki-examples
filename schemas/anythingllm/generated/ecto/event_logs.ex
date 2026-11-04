defmodule AnythingLLM.EventLogs do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          event: String.t(),
          metadata: String.t() | nil,
          user_id: integer() | nil,
          occurred_at: DateTime.t()
        }

  schema "event_logs" do
    field(:event, :string)
    field(:metadata, :string)
    field(:user_id, :integer, source: :userId)
    field(:occurred_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :occurredAt)
  end
end