defmodule OpenPanel.Invite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          project_access: map(),
          project_scope: atom(),
          expires_at: DateTime.t(),
          role: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by: OpenPanel.User.t() | nil,
          organization: OpenPanel.Organization.t() | nil
        }

  schema "invites" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:project_access, :map, source: :projectAccess)
    field(:project_scope, Ecto.Enum, values: [:all, :selected], default: :all, source: :projectScope)
    field(:expires_at, OpenPanel.PrismaDateTime, source: :expiresAt)
    field(:role, :string)
    field(:created_by_id, :string, source: :createdById)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:created_by, OpenPanel.User, foreign_key: :created_by_id, define_field: false, type: :string)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end