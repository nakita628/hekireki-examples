defmodule InboxZero.ScimUser do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          provisioning_domain_id: String.t(),
          connection_user_key: String.t(),
          user_name: String.t(),
          user_name_key: String.t(),
          primary_email: String.t(),
          work_email_value_index: String.t(),
          email_value_index: String.t(),
          display_name: String.t(),
          formatted_name: String.t(),
          given_name: String.t() | nil,
          family_name: String.t() | nil,
          serialized_emails: String.t(),
          serialized_attributes: String.t() | nil,
          external_id: String.t() | nil,
          external_id_key: String.t() | nil,
          active: boolean(),
          order_key: String.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil,
          scim_projection_grants: [InboxZero.ScimProjectionGrant.t()],
          scim_group_members: [InboxZero.ScimGroupMember.t()]
        }

  schema "scimUser" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:provisioning_domain_id, :string, source: :provisioningDomainId)
    field(:connection_user_key, :string, source: :connectionUserKey)
    field(:user_name, :string, source: :userName)
    field(:user_name_key, :string, source: :userNameKey)
    field(:primary_email, :string, source: :primaryEmail)
    field(:work_email_value_index, :string, source: :workEmailValueIndex)
    field(:email_value_index, :string, source: :emailValueIndex)
    field(:display_name, :string, source: :displayName)
    field(:formatted_name, :string, source: :formattedName)
    field(:given_name, :string, source: :givenName)
    field(:family_name, :string, source: :familyName)
    field(:serialized_emails, :string, source: :serializedEmails)
    field(:serialized_attributes, :string, source: :serializedAttributes)
    field(:external_id, :string, source: :externalId)
    field(:external_id_key, :string, source: :externalIdKey)
    field(:active, :boolean)
    field(:order_key, :string, source: :orderKey)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:updated_at, InboxZero.PrismaDateTime, source: :updatedAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:scim_projection_grants, InboxZero.ScimProjectionGrant, foreign_key: :scim_user_id, references: :id)
    has_many(:scim_group_members, InboxZero.ScimGroupMember, foreign_key: :scim_user_id, references: :id)
  end
end