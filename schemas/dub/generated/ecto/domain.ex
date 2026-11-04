defmodule Dub.Domain do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          verified: boolean(),
          placeholder: String.t() | nil,
          expired_url: String.t() | nil,
          not_found_url: String.t() | nil,
          primary: boolean(),
          archived: boolean(),
          last_checked: DateTime.t(),
          logo: String.t() | nil,
          apple_app_site_association: map() | nil,
          asset_links: map() | nil,
          deepview_data: map() | nil,
          link_retention_days: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Dub.Project.t() | nil,
          registered_domain: Dub.RegisteredDomain.t() | nil,
          links: [Dub.Link.t()]
        }

  schema "Domain" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:verified, :boolean, default: false)
    field(:placeholder, :string)
    field(:expired_url, :string, source: :expiredUrl)
    field(:not_found_url, :string, source: :notFoundUrl)
    field(:primary, :boolean, default: false)
    field(:archived, :boolean, default: false)
    field(:last_checked, Dub.PrismaDateTime, autogenerate: true, source: :lastChecked)
    field(:logo, :string)
    field(:apple_app_site_association, :map, source: :appleAppSiteAssociation)
    field(:asset_links, :map, source: :assetLinks)
    field(:deepview_data, :map, default: %{}, source: :deepviewData)
    field(:link_retention_days, :integer, source: :linkRetentionDays)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_one(:registered_domain, Dub.RegisteredDomain, foreign_key: :domain_id, references: :id)
    has_many(:links, Dub.Link, foreign_key: :domain, references: :slug)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end