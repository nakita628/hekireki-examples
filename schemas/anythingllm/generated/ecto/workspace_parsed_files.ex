defmodule AnythingLLM.WorkspaceParsedFiles do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          filename: String.t(),
          metadata: String.t() | nil,
          token_count_estimate: integer() | nil,
          inserted_at: DateTime.t(),
          workspace: AnythingLLM.Workspaces.t() | nil,
          user: AnythingLLM.Users.t() | nil,
          thread: AnythingLLM.WorkspaceThreads.t() | nil
        }

  schema "workspace_parsed_files" do
    field(:filename, :string)
    field(:metadata, :string)
    field(:token_count_estimate, :integer, default: 0, source: :tokenCountEstimate)
    field(:workspace_id, :id, source: :workspaceId)
    field(:user_id, :id, source: :userId)
    field(:thread_id, :id, source: :threadId)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id, define_field: false)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id, define_field: false)
    belongs_to(:thread, AnythingLLM.WorkspaceThreads, foreign_key: :thread_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end