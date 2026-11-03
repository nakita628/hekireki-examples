defmodule Trigger.RealtimeStreamChunk do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          value: String.t(),
          sequence: integer(),
          run_id: String.t(),
          inserted_at: DateTime.t()
        }

  schema "RealtimeStreamChunk" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:value, :string)
    field(:sequence, :integer)
    field(:run_id, :string, source: :runId)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end