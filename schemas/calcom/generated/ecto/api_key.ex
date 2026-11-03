defmodule Calcom.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          note: String.t() | nil,
          expires_at: DateTime.t() | nil,
          last_used_at: DateTime.t() | nil,
          hashed_key: String.t(),
          inserted_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          team: Calcom.Team.t() | nil,
          app: Calcom.App.t() | nil,
          rate_limits: [Calcom.RateLimit.t()]
        }

  schema "ApiKey" do
    field(:id, :string, primary_key: true)
    field(:note, :string)
    field(:expires_at, Calcom.PrismaDateTime, source: :expiresAt)
    field(:last_used_at, Calcom.PrismaDateTime, source: :lastUsedAt)
    field(:hashed_key, :string, source: :hashedKey)
    field(:user_id, :id, source: :userId)
    field(:team_id, :id, source: :teamId)
    field(:app_id, :string, source: :appId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:app, Calcom.App, foreign_key: :app_id, define_field: false, type: :string, references: :slug)
    has_many(:rate_limits, Calcom.RateLimit, foreign_key: :api_key_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end