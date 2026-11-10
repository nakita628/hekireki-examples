defmodule Langfuse.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          note: String.t() | nil,
          public_key: String.t(),
          hashed_secret_key: String.t(),
          fast_hashed_secret_key: String.t() | nil,
          display_secret_key: String.t(),
          last_used_at: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          is_in_app_agent_key: boolean(),
          scope: atom(),
          inserted_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          organization: Langfuse.Organization.t() | nil,
          created_by_user: Langfuse.User.t() | nil,
          user: Langfuse.User.t() | nil,
          created_by_api_key: Langfuse.ApiKey.t() | nil,
          gateway_association: Langfuse.GatewayApiKeyAssociation.t() | nil,
          api_keys_created: [Langfuse.ApiKey.t()],
          role_assignments: [Langfuse.RoleAssignment.t()]
        }

  schema "api_keys" do
    field(:id, :string, primary_key: true)
    field(:note, :string)
    field(:public_key, :string)
    field(:hashed_secret_key, :string)
    field(:fast_hashed_secret_key, :string)
    field(:display_secret_key, :string)
    field(:last_used_at, Langfuse.PrismaDateTime)
    field(:expires_at, Langfuse.PrismaDateTime)
    field(:is_in_app_agent_key, :boolean, default: false)
    field(:scope, Ecto.Enum, values: [:ORGANIZATION, :PROJECT, :USER], default: :PROJECT)
    field(:org_id, :string, source: :organization_id)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:organization, Langfuse.Organization, foreign_key: :org_id, define_field: false, type: :string)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by_user_id, type: :string)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    belongs_to(:created_by_api_key, Langfuse.ApiKey, foreign_key: :created_by_api_key_id, type: :string)
    has_one(:gateway_association, Langfuse.GatewayApiKeyAssociation, foreign_key: :api_key_id, references: :id)
    has_many(:api_keys_created, Langfuse.ApiKey, foreign_key: :created_by_api_key_id, references: :id)
    has_many(:role_assignments, Langfuse.RoleAssignment, foreign_key: :api_key_id, references: :id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end