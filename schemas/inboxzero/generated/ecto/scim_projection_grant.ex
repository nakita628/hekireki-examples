defmodule InboxZero.ScimProjectionGrant do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          provisioning_domain_id: String.t(),
          source_kind: String.t(),
          source_id: String.t(),
          source_value: String.t() | nil,
          role: String.t(),
          grant_key: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          scim_user: InboxZero.ScimUser.t() | nil,
          user: InboxZero.User.t() | nil
        }

  schema "scimProjectionGrant" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:provisioning_domain_id, :string, source: :provisioningDomainId)
    field(:source_kind, :string, source: :sourceKind)
    field(:source_id, :string, source: :sourceId)
    field(:source_value, :string, source: :sourceValue)
    field(:role, :string)
    field(:grant_key, :string, source: :grantKey)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:updated_at, InboxZero.PrismaDateTime, source: :updatedAt)
    field(:scim_user_id, :string, source: :scimUserId)
    field(:user_id, :string, source: :userId)
    belongs_to(:scim_user, InboxZero.ScimUser, foreign_key: :scim_user_id, define_field: false, type: :string)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end