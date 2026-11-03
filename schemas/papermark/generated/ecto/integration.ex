defmodule Papermark.Integration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

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
          category: String.t() | nil,
          coming_soon: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          installed_integrations: [Papermark.InstalledIntegration.t()]
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
    field(:category, :string)
    field(:coming_soon, :boolean, default: false, source: :comingSoon)
    has_many(:installed_integrations, Papermark.InstalledIntegration, foreign_key: :integration_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end