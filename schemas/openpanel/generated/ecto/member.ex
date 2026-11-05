defmodule OpenPanel.Member do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: String.t(),
          email: String.t(),
          project_scope: atom(),
          meta: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: OpenPanel.User.t() | nil,
          invited_by: OpenPanel.User.t() | nil,
          organization: OpenPanel.Organization.t() | nil,
          projects: [OpenPanel.MemberProject.t()]
        }

  schema "members" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:role, :string)
    field(:email, :string)
    field(:project_scope, Ecto.Enum, values: [:all, :selected], default: :all, source: :projectScope)
    field(:meta, :map)
    field(:user_id, :string, source: :userId)
    field(:invited_by_id, :string, source: :invitedById)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:user, OpenPanel.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:invited_by, OpenPanel.User, foreign_key: :invited_by_id, define_field: false, type: :string)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:projects, OpenPanel.MemberProject, foreign_key: :member_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end