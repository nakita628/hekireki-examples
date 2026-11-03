defmodule Langfuse.RoleAssignment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          owner_id: String.t(),
          principal_id: String.t(),
          role_id: String.t(),
          system_role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          organization: Langfuse.Organization.t() | nil,
          user: Langfuse.User.t() | nil,
          api_key: Langfuse.ApiKey.t() | nil
        }

  schema "role_assignments" do
    field(:id, :string, primary_key: true)
    field(:owner_id, :string)
    field(:principal_id, :string)
    field(:role_id, :string)
    field(:system_role, Ecto.Enum, values: [:OWNER, :ADMIN, :MEMBER, :VIEWER, :NONE, :LEGACY_PROJECT_API_KEY, :LEGACY_ORGANIZATION_API_KEY, :SCORES_INGEST, :INGEST, :AI_GATEWAY])
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:organization, Langfuse.Organization, foreign_key: :org_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    belongs_to(:api_key, Langfuse.ApiKey, foreign_key: :api_key_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end