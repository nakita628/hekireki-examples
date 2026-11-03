defmodule Langfuse.ProjectMembership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          user_id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization_membership: Langfuse.OrganizationMembership.t() | nil,
          project: Langfuse.Project.t() | nil,
          user: Langfuse.User.t() | nil
        }

  schema "project_memberships" do
    field(:role, Ecto.Enum, values: [:OWNER, :ADMIN, :MEMBER, :VIEWER, :NONE, :AI_GATEWAY])
    field(:project_id, :string, primary_key: true)
    field(:user_id, :string, primary_key: true)
    belongs_to(:organization_membership, Langfuse.OrganizationMembership, foreign_key: :org_membership_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end