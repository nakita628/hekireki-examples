defmodule LangWatch.RoleBinding do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: atom(),
          scope_type: atom(),
          scope_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          user: LangWatch.User.t() | nil,
          group: LangWatch.Group.t() | nil,
          api_key: LangWatch.ApiKey.t() | nil,
          custom_role: LangWatch.CustomRole.t() | nil
        }

  schema "RoleBinding" do
    field(:id, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER, :VIEWER, :CUSTOM])
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:organization_id, :string, source: :organizationId)
    field(:user_id, :string, source: :userId)
    field(:group_id, :string, source: :groupId)
    field(:api_key_id, :string, source: :apiKeyId)
    field(:custom_role_id, :string, source: :customRoleId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:group, LangWatch.Group, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:api_key, LangWatch.ApiKey, foreign_key: :api_key_id, define_field: false, type: :string)
    belongs_to(:custom_role, LangWatch.CustomRole, foreign_key: :custom_role_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end