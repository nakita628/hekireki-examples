defmodule Dub.OAuthApp do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          client_id: String.t(),
          hashed_client_secret: String.t(),
          partial_client_secret: String.t(),
          redirect_uris: map(),
          pkce: boolean(),
          integration: Dub.Integration.t() | nil,
          o_auth_codes: [Dub.OAuthCode.t()]
        }

  schema "OAuthApp" do
    field(:id, :string, primary_key: true)
    field(:client_id, :string, source: :clientId)
    field(:hashed_client_secret, :string, source: :hashedClientSecret)
    field(:partial_client_secret, :string, source: :partialClientSecret)
    field(:redirect_uris, :map, source: :redirectUris)
    field(:pkce, :boolean, default: false)
    field(:integration_id, :string, source: :integrationId)
    belongs_to(:integration, Dub.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    has_many(:o_auth_codes, Dub.OAuthCode, foreign_key: :client_id, references: :client_id)
  end
end