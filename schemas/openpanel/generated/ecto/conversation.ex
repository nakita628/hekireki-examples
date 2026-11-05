defmodule OpenPanel.Conversation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil,
          organization: OpenPanel.Organization.t() | nil,
          user: OpenPanel.User.t() | nil,
          messages: [OpenPanel.ChatMessage.t()]
        }

  schema "conversations" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:title, :string)
    field(:project_id, :string, source: :projectId)
    field(:organization_id, :string, source: :organizationId)
    field(:user_id, :string, source: :userId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:user, OpenPanel.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:messages, OpenPanel.ChatMessage, foreign_key: :conversation_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end