defmodule Langfuse.OrganizationMembership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Langfuse.Organization.t() | nil,
          user: Langfuse.User.t() | nil,
          project_memberships: [Langfuse.ProjectMembership.t()]
        }

  schema "organization_memberships" do
    field(:id, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:OWNER, :ADMIN, :MEMBER, :VIEWER, :NONE, :AI_GATEWAY])
    belongs_to(:organization, Langfuse.Organization, foreign_key: :org_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    has_many(:project_memberships, Langfuse.ProjectMembership, foreign_key: :org_membership_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end