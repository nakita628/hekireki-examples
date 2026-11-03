defmodule Formbricks.OauthClient do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          client_id: String.t(),
          client_secret: String.t() | nil,
          disabled: boolean() | nil,
          skip_consent: boolean() | nil,
          enable_end_session: boolean() | nil,
          subject_type: String.t() | nil,
          scopes: [String.t()],
          created_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          name: String.t() | nil,
          uri: String.t() | nil,
          icon: String.t() | nil,
          contacts: [String.t()],
          tos: String.t() | nil,
          policy: String.t() | nil,
          software_id: String.t() | nil,
          software_version: String.t() | nil,
          software_statement: String.t() | nil,
          redirect_uris: [String.t()],
          post_logout_redirect_uris: [String.t()],
          token_endpoint_auth_method: String.t() | nil,
          grant_types: [String.t()],
          response_types: [String.t()],
          public: boolean() | nil,
          type: String.t() | nil,
          require_pkce: boolean() | nil,
          reference_id: String.t() | nil,
          metadata: map() | nil,
          client_discovery_id: String.t() | nil,
          client_credentials_scopes: [String.t()],
          backchannel_logout_uri: String.t() | nil,
          backchannel_logout_session_required: boolean() | nil,
          application_type: String.t() | nil,
          jwks: String.t() | nil,
          jwks_uri: String.t() | nil,
          dpop_bound_access_tokens: boolean() | nil,
          user: Formbricks.User.t() | nil,
          access_tokens: [Formbricks.OauthAccessToken.t()],
          refresh_tokens: [Formbricks.OauthRefreshToken.t()],
          consents: [Formbricks.OauthConsent.t()],
          resources: [Formbricks.OauthClientResource.t()]
        }

  schema "oauthClient" do
    field(:id, :string, primary_key: true)
    field(:client_id, :string, source: :clientId)
    field(:client_secret, :string, source: :clientSecret)
    field(:disabled, :boolean, default: false)
    field(:skip_consent, :boolean, source: :skipConsent)
    field(:enable_end_session, :boolean, source: :enableEndSession)
    field(:subject_type, :string, source: :subjectType)
    field(:scopes, {:array, :string})
    field(:created_at, Formbricks.PrismaDateTime, source: :createdAt)
    field(:updated_at, Formbricks.PrismaDateTime, source: :updatedAt)
    field(:name, :string)
    field(:uri, :string)
    field(:icon, :string)
    field(:contacts, {:array, :string})
    field(:tos, :string)
    field(:policy, :string)
    field(:software_id, :string, source: :softwareId)
    field(:software_version, :string, source: :softwareVersion)
    field(:software_statement, :string, source: :softwareStatement)
    field(:redirect_uris, {:array, :string}, source: :redirectUris)
    field(:post_logout_redirect_uris, {:array, :string}, source: :postLogoutRedirectUris)
    field(:token_endpoint_auth_method, :string, source: :tokenEndpointAuthMethod)
    field(:grant_types, {:array, :string}, source: :grantTypes)
    field(:response_types, {:array, :string}, source: :responseTypes)
    field(:public, :boolean)
    field(:type, :string)
    field(:require_pkce, :boolean, source: :requirePKCE)
    field(:reference_id, :string, source: :referenceId)
    field(:metadata, :map)
    field(:client_discovery_id, :string, source: :clientDiscoveryId)
    field(:client_credentials_scopes, {:array, :string}, source: :clientCredentialsScopes)
    field(:backchannel_logout_uri, :string, source: :backchannelLogoutUri)
    field(:backchannel_logout_session_required, :boolean, source: :backchannelLogoutSessionRequired)
    field(:application_type, :string, source: :applicationType)
    field(:jwks, :string)
    field(:jwks_uri, :string, source: :jwksUri)
    field(:dpop_bound_access_tokens, :boolean, default: false, source: :dpopBoundAccessTokens)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:access_tokens, Formbricks.OauthAccessToken, foreign_key: :client_id, references: :client_id)
    has_many(:refresh_tokens, Formbricks.OauthRefreshToken, foreign_key: :client_id, references: :client_id)
    has_many(:consents, Formbricks.OauthConsent, foreign_key: :client_id, references: :client_id)
    has_many(:resources, Formbricks.OauthClientResource, foreign_key: :client_id, references: :client_id)
  end
end