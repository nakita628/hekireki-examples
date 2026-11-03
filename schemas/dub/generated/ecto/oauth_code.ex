defmodule Dub.OAuthCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code: String.t(),
          scopes: String.t() | nil,
          redirect_uri: String.t(),
          code_challenge: String.t() | nil,
          code_challenge_method: String.t() | nil,
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          o_auth_app: Dub.OAuthApp.t() | nil,
          user: Dub.User.t() | nil,
          project: Dub.Project.t() | nil
        }

  schema "OAuthCode" do
    field(:id, :string, primary_key: true)
    field(:code, :string)
    field(:scopes, :string)
    field(:redirect_uri, :string, source: :redirectUri)
    field(:code_challenge, :string, source: :codeChallenge)
    field(:code_challenge_method, :string, source: :codeChallengeMethod)
    field(:expires_at, Dub.PrismaDateTime, source: :expiresAt)
    field(:client_id, :string, source: :clientId)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:o_auth_app, Dub.OAuthApp, foreign_key: :client_id, define_field: false, type: :string, references: :client_id)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end