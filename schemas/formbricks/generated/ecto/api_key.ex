defmodule Formbricks.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          created_by: String.t() | nil,
          last_used_at: DateTime.t() | nil,
          label: String.t(),
          hashed_key: String.t(),
          lookup_hash: String.t() | nil,
          organization_access: map(),
          inserted_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          api_key_workspaces: [Formbricks.ApiKeyWorkspace.t()]
        }

  schema "ApiKey" do
    field(:id, :string, primary_key: true)
    field(:created_by, :string, source: :createdBy)
    field(:last_used_at, Formbricks.PrismaDateTime, source: :lastUsedAt)
    field(:label, :string)
    field(:hashed_key, :string, source: :hashedKey)
    field(:lookup_hash, :string, source: :lookupHash)
    field(:organization_access, :map, default: %{}, source: :organizationAccess)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:api_key_workspaces, Formbricks.ApiKeyWorkspace, foreign_key: :api_key_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end