defmodule InboxZero.ScimGroupMember do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          membership_key: String.t(),
          created_at: DateTime.t(),
          scim_group: InboxZero.ScimGroup.t() | nil,
          scim_user: InboxZero.ScimUser.t() | nil
        }

  schema "scimGroupMember" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:membership_key, :string, source: :membershipKey)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:group_id, :string, source: :groupId)
    field(:scim_user_id, :string, source: :scimUserId)
    belongs_to(:scim_group, InboxZero.ScimGroup, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:scim_user, InboxZero.ScimUser, foreign_key: :scim_user_id, define_field: false, type: :string)
  end
end