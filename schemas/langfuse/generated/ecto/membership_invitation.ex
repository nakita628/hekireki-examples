defmodule Langfuse.MembershipInvitation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          org_role: atom(),
          project_role: atom() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Langfuse.Organization.t() | nil,
          project: Langfuse.Project.t() | nil,
          invited_by_user: Langfuse.User.t() | nil
        }

  schema "membership_invitations" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:org_role, Ecto.Enum, values: [:OWNER, :ADMIN, :MEMBER, :VIEWER, :NONE, :AI_GATEWAY])
    field(:project_role, Ecto.Enum, values: [:OWNER, :ADMIN, :MEMBER, :VIEWER, :NONE, :AI_GATEWAY])
    belongs_to(:organization, Langfuse.Organization, foreign_key: :org_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:invited_by_user, Langfuse.User, foreign_key: :invited_by_user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end