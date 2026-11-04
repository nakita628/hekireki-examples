defmodule AnythingLLM.WorkspaceThreads do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          slug: String.t(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          workspace: AnythingLLM.Workspaces.t() | nil,
          user: AnythingLLM.Users.t() | nil,
          workspace_parsed_files: [AnythingLLM.WorkspaceParsedFiles.t()]
        }

  schema "workspace_threads" do
    field(:name, :string)
    field(:slug, :string)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id)
    has_many(:workspace_parsed_files, AnythingLLM.WorkspaceParsedFiles, foreign_key: :thread_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end