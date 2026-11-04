defmodule Dub.Integration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          description: String.t() | nil,
          readme: String.t() | nil,
          developer: String.t(),
          website: String.t(),
          logo: String.t() | nil,
          screenshots: map() | nil,
          verified: boolean(),
          install_url: String.t() | nil,
          guide_url: String.t() | nil,
          category: String.t() | nil,
          coming_soon: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil,
          project: Dub.Project.t() | nil,
          o_auth_app: Dub.OAuthApp.t() | nil,
          installations: [Dub.InstalledIntegration.t()]
        }

  schema "Integration" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:description, :string)
    field(:readme, :string)
    field(:developer, :string)
    field(:website, :string)
    field(:logo, :string)
    field(:screenshots, :map)
    field(:verified, :boolean, default: false)
    field(:install_url, :string, source: :installUrl)
    field(:guide_url, :string, source: :guideUrl)
    field(:category, :string)
    field(:coming_soon, :boolean, default: false, source: :comingSoon)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_one(:o_auth_app, Dub.OAuthApp, foreign_key: :integration_id, references: :id)
    has_many(:installations, Dub.InstalledIntegration, foreign_key: :integration_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end