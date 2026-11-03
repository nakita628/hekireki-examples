defmodule Papermark.OAuthClient do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          client_id: String.t(),
          client_name: String.t(),
          client_uri: String.t() | nil,
          logo_uri: String.t() | nil,
          redirect_uris: [String.t()],
          grant_types: [String.t()],
          scopes: String.t(),
          token_auth_method: String.t(),
          hashed_secret: String.t() | nil,
          registration_type: String.t(),
          metadata_url: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          tokens: [Papermark.RestrictedToken.t()]
        }

  schema "OAuthClient" do
    field(:id, :string, primary_key: true)
    field(:client_id, :string, source: :clientId)
    field(:client_name, :string, source: :clientName)
    field(:client_uri, :string, source: :clientUri)
    field(:logo_uri, :string, source: :logoUri)
    field(:redirect_uris, {:array, :string}, source: :redirectUris)
    field(:grant_types, {:array, :string}, source: :grantTypes)
    field(:scopes, :string)
    field(:token_auth_method, :string, source: :tokenAuthMethod)
    field(:hashed_secret, :string, source: :hashedSecret)
    field(:registration_type, :string, source: :registrationType)
    field(:metadata_url, :string, source: :metadataUrl)
    has_many(:tokens, Papermark.RestrictedToken, foreign_key: :client_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end