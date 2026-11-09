defmodule Jitsu.OAuthClient do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          client_secret_hash: String.t(),
          name: String.t(),
          redirect_uris: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          tokens: [Jitsu.UserApiToken.t()]
        }

  schema "OAuthClient" do
    field(:id, :string, primary_key: true)
    field(:client_secret_hash, :string, source: :clientSecretHash)
    field(:name, :string)
    field(:redirect_uris, {:array, :string}, source: :redirectUris)
    has_many(:tokens, Jitsu.UserApiToken, foreign_key: :oauth_client_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end