defmodule InboxZero.OauthConsent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          reference_id: String.t() | nil,
          resources: [String.t()],
          requested_user_info_claims: [String.t()],
          scopes: [String.t()],
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          client: InboxZero.OauthClient.t() | nil,
          user: InboxZero.User.t() | nil
        }

  schema "OauthConsent" do
    field(:id, :string, primary_key: true)
    field(:reference_id, :string, source: :referenceId)
    field(:resources, {:array, :string})
    field(:requested_user_info_claims, {:array, :string}, source: :requestedUserInfoClaims)
    field(:scopes, {:array, :string})
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:updated_at, InboxZero.PrismaDateTime, source: :updatedAt)
    field(:client_id, :string, source: :clientId)
    field(:user_id, :string, source: :userId)
    belongs_to(:client, InboxZero.OauthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end