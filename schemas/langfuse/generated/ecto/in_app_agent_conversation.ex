defmodule Langfuse.InAppAgentConversation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          id: String.t(),
          title: String.t() | nil,
          renamed_by_user_at: DateTime.t() | nil,
          visibility_scope: atom(),
          provider_session_id: String.t() | nil,
          always_allowed_tools: [String.t()],
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          created_by_user: Langfuse.User.t() | nil
        }

  schema "in_app_agent_conversations" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:renamed_by_user_at, Langfuse.PrismaDateTime)
    field(:visibility_scope, Ecto.Enum, values: [:PERSONAL, :PROJECT], default: :PERSONAL)
    field(:provider_session_id, :string)
    field(:always_allowed_tools, {:array, :string})
    field(:deleted_at, Langfuse.PrismaDateTime)
    field(:project_id, :string, primary_key: true)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by_user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end