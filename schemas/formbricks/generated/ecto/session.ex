defmodule Formbricks.Session do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          session_token: String.t(),
          expires: DateTime.t(),
          ip_address: String.t() | nil,
          user_agent: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Formbricks.User.t() | nil,
          oauth_access_tokens: [Formbricks.OauthAccessToken.t()],
          oauth_refresh_tokens: [Formbricks.OauthRefreshToken.t()]
        }

  schema "Session" do
    field(:id, :string, primary_key: true)
    field(:session_token, :string, source: :sessionToken)
    field(:expires, Formbricks.PrismaDateTime)
    field(:ip_address, :string, source: :ipAddress)
    field(:user_agent, :string, source: :userAgent)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
    has_many(:oauth_access_tokens, Formbricks.OauthAccessToken, foreign_key: :session_id)
    has_many(:oauth_refresh_tokens, Formbricks.OauthRefreshToken, foreign_key: :session_id)
    timestamps(inserted_at_source: :created_at)
  end
end