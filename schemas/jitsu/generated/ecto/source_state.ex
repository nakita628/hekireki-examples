defmodule Jitsu.SourceState do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          sync_id: String.t(),
          stream: String.t(),
          state: map(),
          timestamp: DateTime.t()
        }

  schema "source_state" do
    field(:sync_id, :string, primary_key: true)
    field(:stream, :string, primary_key: true)
    field(:state, :map)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
  end
end