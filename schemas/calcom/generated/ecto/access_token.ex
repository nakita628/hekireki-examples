defmodule Calcom.AccessToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          secret: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          owner: Calcom.User.t() | nil,
          client: Calcom.PlatformOAuthClient.t() | nil
        }

  schema "AccessToken" do
    field(:secret, :string)
    field(:expires_at, Calcom.PrismaDateTime, source: :expiresAt)
    field(:user_id, :id, source: :userId)
    field(:platform_oauth_client_id, :string, source: :platformOAuthClientId)
    belongs_to(:owner, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:client, Calcom.PlatformOAuthClient, foreign_key: :platform_oauth_client_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end