defmodule LangWatch.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          lookup_id: String.t(),
          hashed_secret: String.t(),
          permission_mode: String.t(),
          created_by_user_id: String.t() | nil,
          expires_at: DateTime.t() | nil,
          last_used_at: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          revocation_cause: String.t() | nil,
          parent_api_key_id: String.t() | nil,
          ingest_source_type: String.t() | nil,
          ingestion_template_id: String.t() | nil,
          created_by_device_label: String.t() | nil,
          is_system_managed: boolean(),
          created_by_browser_session_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: LangWatch.User.t() | nil,
          organization: LangWatch.Organization.t() | nil,
          role_bindings: [LangWatch.RoleBinding.t()]
        }

  schema "ApiKey" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:lookup_id, :string, source: :lookupId)
    field(:hashed_secret, :string, source: :hashedSecret)
    field(:permission_mode, :string, default: "all", source: :permissionMode)
    field(:created_by_user_id, :string, source: :createdByUserId)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:last_used_at, LangWatch.PrismaDateTime, source: :lastUsedAt)
    field(:revoked_at, LangWatch.PrismaDateTime, source: :revokedAt)
    field(:revocation_cause, :string, source: :revocationCause)
    field(:parent_api_key_id, :string, source: :parentApiKeyId)
    field(:ingest_source_type, :string, source: :ingestSourceType)
    field(:ingestion_template_id, :string, source: :ingestionTemplateId)
    field(:created_by_device_label, :string, source: :createdByDeviceLabel)
    field(:is_system_managed, :boolean, default: false, source: :isSystemManaged)
    field(:created_by_browser_session_id, :string, source: :createdByBrowserSessionId)
    field(:user_id, :string, source: :userId)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:role_bindings, LangWatch.RoleBinding, foreign_key: :api_key_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end