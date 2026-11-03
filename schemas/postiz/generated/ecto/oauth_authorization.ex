defmodule Postiz.OAuthAuthorization do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Postiz.PrismaDateTime, autogenerate: {Postiz.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          access_token: String.t() | nil,
          authorization_code: String.t() | nil,
          code_expires_at: DateTime.t() | nil,
          code_challenge: String.t() | nil,
          code_challenge_method: String.t() | nil,
          redirect_uri: String.t() | nil,
          revoked_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          oauth_app: Postiz.OAuthApp.t() | nil,
          user: Postiz.User.t() | nil,
          organization: Postiz.Organization.t() | nil
        }

  schema "OAuthAuthorization" do
    field(:access_token, :string, source: :accessToken)
    field(:authorization_code, :string, source: :authorizationCode)
    field(:code_expires_at, Postiz.PrismaDateTime, source: :codeExpiresAt)
    field(:code_challenge, :string, source: :codeChallenge)
    field(:code_challenge_method, :string, source: :codeChallengeMethod)
    field(:redirect_uri, :string, source: :redirectUri)
    field(:revoked_at, Postiz.PrismaDateTime, source: :revokedAt)
    field(:oauth_app_id, :binary_id, source: :oauthAppId)
    field(:user_id, :binary_id, source: :userId)
    field(:organization_id, :binary_id, source: :organizationId)
    belongs_to(:oauth_app, Postiz.OAuthApp, foreign_key: :oauth_app_id, define_field: false)
    belongs_to(:user, Postiz.User, foreign_key: :user_id, define_field: false)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end