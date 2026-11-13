defmodule Jitsu.UserApiToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          last_used: DateTime.t() | nil,
          hint: String.t(),
          hash: String.t(),
          type: String.t() | nil,
          name: String.t() | nil,
          expires_at: DateTime.t() | nil,
          workspace_id: String.t() | nil,
          role: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Jitsu.UserProfile.t() | nil,
          oauth_client: Jitsu.OAuthClient.t() | nil,
          access_tokens: [Jitsu.OAuthAccessToken.t()]
        }

  schema "UserApiToken" do
    field(:id, :string, primary_key: true)
    field(:last_used, Jitsu.PrismaDateTime, source: :lastUsed)
    field(:hint, :string)
    field(:hash, :string)
    field(:type, :string)
    field(:name, :string)
    field(:expires_at, Jitsu.PrismaDateTime, source: :expiresAt)
    field(:workspace_id, :string, source: :workspaceId)
    field(:role, :string)
    field(:user_id, :string, source: :userId)
    field(:oauth_client_id, :string, source: :oauthClientId)
    belongs_to(:user, Jitsu.UserProfile, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:oauth_client, Jitsu.OAuthClient, foreign_key: :oauth_client_id, define_field: false, type: :string)
    has_many(:access_tokens, Jitsu.OAuthAccessToken, foreign_key: :refresh_token_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end