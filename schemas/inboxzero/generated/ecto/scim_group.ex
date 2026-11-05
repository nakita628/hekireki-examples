defmodule InboxZero.ScimGroup do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          provisioning_domain_id: String.t(),
          revision: integer(),
          display_name: String.t(),
          display_name_key: String.t(),
          external_id: String.t() | nil,
          external_id_key: String.t() | nil,
          order_key: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          scim_group_members: [InboxZero.ScimGroupMember.t()]
        }

  schema "scimGroup" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:provisioning_domain_id, :string, source: :provisioningDomainId)
    field(:revision, :integer, default: 0)
    field(:display_name, :string, source: :displayName)
    field(:display_name_key, :string, source: :displayNameKey)
    field(:external_id, :string, source: :externalId)
    field(:external_id_key, :string, source: :externalIdKey)
    field(:order_key, :string, source: :orderKey)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:updated_at, InboxZero.PrismaDateTime, source: :updatedAt)
    has_many(:scim_group_members, InboxZero.ScimGroupMember, foreign_key: :group_id, references: :id)
  end
end