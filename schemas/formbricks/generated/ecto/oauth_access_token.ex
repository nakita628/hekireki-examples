defmodule Formbricks.OauthAccessToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          reference_id: String.t() | nil,
          expires_at: DateTime.t(),
          created_at: DateTime.t(),
          scopes: [String.t()],
          authorization_code_id: String.t() | nil,
          resources: [String.t()],
          requested_user_info_claims: [String.t()],
          revoked: DateTime.t() | nil,
          confirmation: map() | nil,
          client: Formbricks.OauthClient.t() | nil,
          session: Formbricks.Session.t() | nil,
          user: Formbricks.User.t() | nil,
          refresh: Formbricks.OauthRefreshToken.t() | nil
        }

  schema "oauthAccessToken" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:reference_id, :string, source: :referenceId)
    field(:expires_at, Formbricks.PrismaDateTime, source: :expiresAt)
    field(:created_at, Formbricks.PrismaDateTime, source: :createdAt)
    field(:scopes, {:array, :string})
    field(:authorization_code_id, :string, source: :authorizationCodeId)
    field(:resources, {:array, :string})
    field(:requested_user_info_claims, {:array, :string}, source: :requestedUserInfoClaims)
    field(:revoked, Formbricks.PrismaDateTime)
    field(:confirmation, :map)
    field(:client_id, :string, source: :clientId)
    field(:session_id, :string, source: :sessionId)
    field(:user_id, :string, source: :userId)
    field(:refresh_id, :string, source: :refreshId)
    belongs_to(:client, Formbricks.OauthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:session, Formbricks.Session, foreign_key: :session_id, define_field: false, type: :string)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:refresh, Formbricks.OauthRefreshToken, foreign_key: :refresh_id, define_field: false, type: :string)
  end
end