defmodule Dub.OAuthRefreshToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          hashed_refresh_token: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          access_token: Dub.RestrictedToken.t() | nil,
          installed_integration: Dub.InstalledIntegration.t() | nil
        }

  schema "OAuthRefreshToken" do
    field(:id, :string, primary_key: true)
    field(:hashed_refresh_token, :string, source: :hashedRefreshToken)
    field(:expires_at, Dub.PrismaDateTime, source: :expiresAt)
    field(:access_token_id, :string, source: :accessTokenId)
    field(:installation_id, :string, source: :installationId)
    belongs_to(:access_token, Dub.RestrictedToken, foreign_key: :access_token_id, define_field: false, type: :string)
    belongs_to(:installed_integration, Dub.InstalledIntegration, foreign_key: :installation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end