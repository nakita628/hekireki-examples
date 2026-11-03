defmodule Papermark.RestrictedToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          hashed_key: String.t(),
          partial_key: String.t(),
          scopes: String.t() | nil,
          mode: String.t(),
          source: String.t(),
          subject_type: String.t(),
          expires: DateTime.t() | nil,
          last_used: DateTime.t() | nil,
          rate_limit: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil,
          oauth_client: Papermark.OAuthClient.t() | nil
        }

  schema "RestrictedToken" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:hashed_key, :string, source: :hashedKey)
    field(:partial_key, :string, source: :partialKey)
    field(:scopes, :string)
    field(:mode, :string, default: "live")
    field(:source, :string, default: "dashboard")
    field(:subject_type, :string, default: "user", source: :subjectType)
    field(:expires, Papermark.PrismaDateTime)
    field(:last_used, Papermark.PrismaDateTime, source: :lastUsed)
    field(:rate_limit, :integer, default: 60, source: :rateLimit)
    field(:user_id, :string, source: :userId)
    field(:team_id, :string, source: :teamId)
    field(:client_id, :string, source: :clientId)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:oauth_client, Papermark.OAuthClient, foreign_key: :client_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end