defmodule AnythingLLM.WorkspaceChats do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          workspace_id: integer(),
          prompt: String.t(),
          response: String.t(),
          include: boolean(),
          thread_id: integer() | nil,
          api_session_id: String.t() | nil,
          last_updated_at: DateTime.t(),
          feedback_score: boolean() | nil,
          memory_processed: boolean() | nil,
          inserted_at: DateTime.t(),
          users: AnythingLLM.Users.t() | nil
        }

  schema "workspace_chats" do
    field(:workspace_id, :integer, source: :workspaceId)
    field(:prompt, :string)
    field(:response, :string)
    field(:include, :boolean, default: true)
    field(:thread_id, :integer)
    field(:api_session_id, :string)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    field(:feedback_score, :boolean, source: :feedbackScore)
    field(:memory_processed, :boolean)
    belongs_to(:users, AnythingLLM.Users, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end