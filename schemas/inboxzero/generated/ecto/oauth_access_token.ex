defmodule InboxZero.OauthAccessToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          reference_id: String.t() | nil,
          authorization_code_id: String.t() | nil,
          resources: [String.t()],
          requested_user_info_claims: [String.t()],
          expires_at: DateTime.t(),
          created_at: DateTime.t(),
          revoked: DateTime.t() | nil,
          confirmation: map() | nil,
          scopes: [String.t()],
          client: InboxZero.OauthClient.t() | nil,
          session: InboxZero.Session.t() | nil,
          user: InboxZero.User.t() | nil,
          refresh: InboxZero.OauthRefreshToken.t() | nil
        }

  schema "OauthAccessToken" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:reference_id, :string, source: :referenceId)
    field(:authorization_code_id, :string, source: :authorizationCodeId)
    field(:resources, {:array, :string})
    field(:requested_user_info_claims, {:array, :string}, source: :requestedUserInfoClaims)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:revoked, InboxZero.PrismaDateTime)
    field(:confirmation, :map)
    field(:scopes, {:array, :string})
    field(:client_id, :string, source: :clientId)
    field(:session_id, :string, source: :sessionId)
    field(:user_id, :string, source: :userId)
    field(:refresh_id, :string, source: :refreshId)
    belongs_to(:client, InboxZero.OauthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:session, InboxZero.Session, foreign_key: :session_id, define_field: false, type: :string)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:refresh, InboxZero.OauthRefreshToken, foreign_key: :refresh_id, define_field: false, type: :string)
  end
end