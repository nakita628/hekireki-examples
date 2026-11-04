defmodule Dub.InstalledIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          credentials: map() | nil,
          settings: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil,
          integration: Dub.Integration.t() | nil,
          project: Dub.Project.t() | nil,
          refresh_tokens: [Dub.OAuthRefreshToken.t()],
          access_tokens: [Dub.RestrictedToken.t()],
          webhooks: [Dub.Webhook.t()]
        }

  schema "InstalledIntegration" do
    field(:id, :string, primary_key: true)
    field(:credentials, :map)
    field(:settings, :map)
    field(:user_id, :string, source: :userId)
    field(:integration_id, :string, source: :integrationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:integration, Dub.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:refresh_tokens, Dub.OAuthRefreshToken, foreign_key: :installation_id, references: :id)
    has_many(:access_tokens, Dub.RestrictedToken, foreign_key: :installation_id, references: :id)
    has_many(:webhooks, Dub.Webhook, foreign_key: :installation_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end