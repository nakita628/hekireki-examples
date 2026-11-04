defmodule AnythingLLM.WorkspaceDocuments do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          doc_id: String.t(),
          filename: String.t(),
          docpath: String.t(),
          metadata: String.t() | nil,
          pinned: boolean() | nil,
          watched: boolean() | nil,
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          workspace: AnythingLLM.Workspaces.t() | nil,
          document_sync_queues: AnythingLLM.DocumentSyncQueues.t() | nil
        }

  schema "workspace_documents" do
    field(:doc_id, :string, source: :docId)
    field(:filename, :string)
    field(:docpath, :string)
    field(:metadata, :string)
    field(:pinned, :boolean, default: false)
    field(:watched, :boolean, default: false)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    field(:workspace_id, :id, source: :workspaceId)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id, define_field: false)
    has_one(:document_sync_queues, AnythingLLM.DocumentSyncQueues, foreign_key: :workspace_doc_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end