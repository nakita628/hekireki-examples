defmodule AnythingLLM.DocumentSyncQueues do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          stale_after_ms: integer(),
          next_sync_at: DateTime.t(),
          last_synced_at: DateTime.t(),
          inserted_at: DateTime.t(),
          workspace_doc: AnythingLLM.WorkspaceDocuments.t() | nil,
          runs: [AnythingLLM.DocumentSyncExecutions.t()]
        }

  schema "document_sync_queues" do
    field(:stale_after_ms, :integer, default: 604800000, source: :staleAfterMs)
    field(:next_sync_at, AnythingLLM.PrismaDateTime, source: :nextSyncAt)
    field(:last_synced_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastSyncedAt)
    field(:workspace_doc_id, :id, source: :workspaceDocId)
    belongs_to(:workspace_doc, AnythingLLM.WorkspaceDocuments, foreign_key: :workspace_doc_id, define_field: false)
    has_many(:runs, AnythingLLM.DocumentSyncExecutions, foreign_key: :queue_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end