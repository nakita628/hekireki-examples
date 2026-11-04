defmodule AnythingLLM.WorkspaceSuggestedMessages do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          heading: String.t(),
          message: String.t(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          workspace: AnythingLLM.Workspaces.t() | nil
        }

  schema "workspace_suggested_messages" do
    field(:heading, :string)
    field(:message, :string)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    field(:workspace_id, :id, source: :workspaceId)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end