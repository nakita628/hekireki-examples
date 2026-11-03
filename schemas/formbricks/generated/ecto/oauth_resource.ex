defmodule Formbricks.OauthResource do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          identifier: String.t(),
          name: String.t(),
          access_token_ttl: integer() | nil,
          refresh_token_ttl: integer() | nil,
          signing_algorithm: String.t() | nil,
          signing_key_id: String.t() | nil,
          allowed_scopes: [String.t()],
          custom_claims: map() | nil,
          dpop_bound_access_tokens_required: boolean() | nil,
          disabled: boolean() | nil,
          created_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          policy_version: integer() | nil,
          metadata: map() | nil,
          clients: [Formbricks.OauthClientResource.t()]
        }

  schema "oauthResource" do
    field(:id, :string, primary_key: true)
    field(:identifier, :string)
    field(:name, :string)
    field(:access_token_ttl, :integer, source: :accessTokenTtl)
    field(:refresh_token_ttl, :integer, source: :refreshTokenTtl)
    field(:signing_algorithm, :string, source: :signingAlgorithm)
    field(:signing_key_id, :string, source: :signingKeyId)
    field(:allowed_scopes, {:array, :string}, source: :allowedScopes)
    field(:custom_claims, :map, source: :customClaims)
    field(:dpop_bound_access_tokens_required, :boolean, default: false, source: :dpopBoundAccessTokensRequired)
    field(:disabled, :boolean, default: false)
    field(:created_at, Formbricks.PrismaDateTime, source: :createdAt)
    field(:updated_at, Formbricks.PrismaDateTime, source: :updatedAt)
    field(:policy_version, :integer, default: 1, source: :policyVersion)
    field(:metadata, :map)
    has_many(:clients, Formbricks.OauthClientResource, foreign_key: :resource_id, references: :identifier)
  end
end