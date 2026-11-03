defmodule Calcom.AccessCode do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          code: String.t(),
          expires_at: DateTime.t(),
          scopes: atom(),
          code_challenge: String.t() | nil,
          code_challenge_method: String.t() | nil,
          client: Calcom.OAuthClient.t() | nil,
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil
        }

  schema "AccessCode" do
    field(:code, :string)
    field(:expires_at, Calcom.PrismaDateTime, source: :expiresAt)
    field(:scopes, {:array, Ecto.Enum}, values: [:READ_BOOKING, :READ_PROFILE])
    field(:code_challenge, :string, source: :codeChallenge)
    field(:code_challenge_method, :string, default: "S256", source: :codeChallengeMethod)
    field(:client_id, :string, source: :clientId)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    belongs_to(:client, Calcom.OAuthClient, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
  end
end