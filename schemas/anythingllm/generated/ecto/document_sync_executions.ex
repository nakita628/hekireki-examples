defmodule AnythingLLM.DocumentSyncExecutions do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          status: String.t(),
          result: String.t() | nil,
          inserted_at: DateTime.t(),
          queue: AnythingLLM.DocumentSyncQueues.t() | nil
        }

  schema "document_sync_executions" do
    field(:status, :string, default: "unknown")
    field(:result, :string)
    field(:queue_id, :id, source: :queueId)
    belongs_to(:queue, AnythingLLM.DocumentSyncQueues, foreign_key: :queue_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end