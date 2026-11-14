defmodule LangWatch.Grant do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          principal_type: atom(),
          principal_id: String.t() | nil,
          role_key: String.t() | nil,
          legacy_role: String.t() | nil,
          source: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          token: String.t() | nil,
          permission: String.t() | nil,
          resource_kind: String.t() | nil,
          project_id: String.t() | nil,
          created_by_user_id: String.t() | nil,
          expires_at: DateTime.t() | nil,
          max_views: integer() | nil,
          condition: map() | nil,
          replaces_grant_id: String.t() | nil,
          occurred_at: DateTime.t(),
          revoked_at: DateTime.t() | nil,
          revoked_reason: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "Grant" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:principal_type, Ecto.Enum, values: [:USER, :API_KEY, :GROUP, :TEAM, :ORGANIZATION, :PROJECT, :ANYONE], source: :principalType)
    field(:principal_id, :string, source: :principalId)
    field(:role_key, :string, source: :roleKey)
    field(:legacy_role, :string, source: :legacyRole)
    field(:source, :string)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT, :RESOURCE, :PLATFORM], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:token, :string)
    field(:permission, :string)
    field(:resource_kind, :string, source: :resourceKind)
    field(:project_id, :string, source: :projectId)
    field(:created_by_user_id, :string, source: :createdByUserId)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:max_views, :integer, source: :maxViews)
    field(:condition, :map)
    field(:replaces_grant_id, :string, source: :replacesGrantId)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:revoked_at, LangWatch.PrismaDateTime, source: :revokedAt)
    field(:revoked_reason, :string, source: :revokedReason)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end