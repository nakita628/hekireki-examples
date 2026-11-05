defmodule InboxZero.ScimIdentityTombstone do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          provisioning_domain_id: String.t(),
          external_id: String.t(),
          external_id_key: String.t(),
          profile: String.t(),
          deleted_at: DateTime.t(),
          user: InboxZero.User.t() | nil
        }

  schema "scimIdentityTombstone" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:provisioning_domain_id, :string, source: :provisioningDomainId)
    field(:external_id, :string, source: :externalId)
    field(:external_id_key, :string, source: :externalIdKey)
    field(:profile, :string)
    field(:deleted_at, InboxZero.PrismaDateTime, source: :deletedAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end