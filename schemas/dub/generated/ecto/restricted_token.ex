defmodule Dub.RestrictedToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          hashed_key: String.t(),
          partial_key: String.t(),
          scopes: String.t() | nil,
          expires: DateTime.t() | nil,
          last_used: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil,
          project: Dub.Project.t() | nil,
          installed_integration: Dub.InstalledIntegration.t() | nil,
          refresh_tokens: [Dub.OAuthRefreshToken.t()]
        }

  schema "RestrictedToken" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:hashed_key, :string, source: :hashedKey)
    field(:partial_key, :string, source: :partialKey)
    field(:scopes, :string)
    field(:expires, Dub.PrismaDateTime)
    field(:last_used, Dub.PrismaDateTime, source: :lastUsed)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    field(:installation_id, :string, source: :installationId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:installed_integration, Dub.InstalledIntegration, foreign_key: :installation_id, define_field: false, type: :string)
    has_many(:refresh_tokens, Dub.OAuthRefreshToken, foreign_key: :access_token_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end