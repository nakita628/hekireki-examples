defmodule Jitsu.OidcProvider do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          issuer: String.t(),
          client_id: String.t(),
          client_secret: String.t(),
          authorization_endpoint: String.t() | nil,
          token_endpoint: String.t() | nil,
          userinfo_endpoint: String.t() | nil,
          jwks_uri: String.t() | nil,
          introspection_endpoint: String.t() | nil,
          scopes: [String.t()],
          audience: String.t() | nil,
          prompt: String.t() | nil,
          username_claim: String.t(),
          email_claim: String.t(),
          name_claim: String.t(),
          group_claim: String.t(),
          enabled: boolean(),
          auto_discovery: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          oidc_login_groups: [Jitsu.OidcLoginGroup.t()]
        }

  schema "OidcProvider" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:issuer, :string)
    field(:client_id, :string, source: :clientId)
    field(:client_secret, :string, source: :clientSecret)
    field(:authorization_endpoint, :string, source: :authorizationEndpoint)
    field(:token_endpoint, :string, source: :tokenEndpoint)
    field(:userinfo_endpoint, :string, source: :userinfoEndpoint)
    field(:jwks_uri, :string, source: :jwksUri)
    field(:introspection_endpoint, :string, source: :introspectionEndpoint)
    field(:scopes, {:array, :string})
    field(:audience, :string)
    field(:prompt, :string)
    field(:username_claim, :string, default: "email", source: :usernameClaim)
    field(:email_claim, :string, default: "email", source: :emailClaim)
    field(:name_claim, :string, default: "name", source: :nameClaim)
    field(:group_claim, :string, default: "groups", source: :groupClaim)
    field(:enabled, :boolean, default: true)
    field(:auto_discovery, :boolean, default: true, source: :autoDiscovery)
    has_many(:oidc_login_groups, Jitsu.OidcLoginGroup, foreign_key: :oidc_provider_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end