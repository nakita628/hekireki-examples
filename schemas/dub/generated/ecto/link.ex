defmodule Dub.Link do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          url: String.t(),
          short_link: String.t(),
          archived: boolean(),
          expires_at: DateTime.t() | nil,
          expired_url: String.t() | nil,
          disabled_at: DateTime.t() | nil,
          password: String.t() | nil,
          track_conversion: boolean(),
          proxy: boolean(),
          title: String.t() | nil,
          description: String.t() | nil,
          image: String.t() | nil,
          video: String.t() | nil,
          utm_source: String.t() | nil,
          utm_medium: String.t() | nil,
          utm_campaign: String.t() | nil,
          utm_term: String.t() | nil,
          utm_content: String.t() | nil,
          rewrite: boolean(),
          link_retention_cleanup_disabled_at: DateTime.t() | nil,
          do_index: boolean(),
          ios: String.t() | nil,
          android: String.t() | nil,
          geo: map() | nil,
          test_variants: map() | nil,
          test_started_at: DateTime.t() | nil,
          test_completed_at: DateTime.t() | nil,
          external_id: String.t() | nil,
          tenant_id: String.t() | nil,
          public_stats: boolean(),
          clicks: integer(),
          leads: integer(),
          conversions: integer(),
          sales: integer(),
          sale_amount: integer(),
          last_clicked: DateTime.t() | nil,
          last_lead_at: DateTime.t() | nil,
          last_conversion_at: DateTime.t() | nil,
          comments: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil,
          project: Dub.Project.t() | nil,
          folder: Dub.Folder.t() | nil,
          short_domain: Dub.Domain.t() | nil,
          dashboard: Dub.Dashboard.t() | nil,
          tags: [Dub.LinkTag.t()],
          webhooks: [Dub.LinkWebhook.t()],
          customers: [Dub.Customer.t()]
        }

  schema "Link" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:url, :string)
    field(:short_link, :string, source: :shortLink)
    field(:archived, :boolean, default: false)
    field(:expires_at, Dub.PrismaDateTime, source: :expiresAt)
    field(:expired_url, :string, source: :expiredUrl)
    field(:disabled_at, Dub.PrismaDateTime, source: :disabledAt)
    field(:password, :string)
    field(:track_conversion, :boolean, default: false, source: :trackConversion)
    field(:proxy, :boolean, default: false)
    field(:title, :string)
    field(:description, :string)
    field(:image, :string)
    field(:video, :string)
    field(:utm_source, :string)
    field(:utm_medium, :string)
    field(:utm_campaign, :string)
    field(:utm_term, :string)
    field(:utm_content, :string)
    field(:rewrite, :boolean, default: false)
    field(:link_retention_cleanup_disabled_at, Dub.PrismaDateTime, source: :linkRetentionCleanupDisabledAt)
    field(:do_index, :boolean, default: false, source: :doIndex)
    field(:ios, :string)
    field(:android, :string)
    field(:geo, :map)
    field(:test_variants, :map, source: :testVariants)
    field(:test_started_at, Dub.PrismaDateTime, source: :testStartedAt)
    field(:test_completed_at, Dub.PrismaDateTime, source: :testCompletedAt)
    field(:external_id, :string, source: :externalId)
    field(:tenant_id, :string, source: :tenantId)
    field(:public_stats, :boolean, default: false, source: :publicStats)
    field(:clicks, :integer, default: 0)
    field(:leads, :integer, default: 0)
    field(:conversions, :integer, default: 0)
    field(:sales, :integer, default: 0)
    field(:sale_amount, :integer, default: 0, source: :saleAmount)
    field(:last_clicked, Dub.PrismaDateTime, source: :lastClicked)
    field(:last_lead_at, Dub.PrismaDateTime, source: :lastLeadAt)
    field(:last_conversion_at, Dub.PrismaDateTime, source: :lastConversionAt)
    field(:comments, :string)
    field(:user_id, :string, source: :userId)
    field(:project_id, :string, source: :projectId)
    field(:folder_id, :string, source: :folderId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:folder, Dub.Folder, foreign_key: :folder_id, define_field: false, type: :string)
    belongs_to(:short_domain, Dub.Domain, foreign_key: :domain, type: :string, references: :slug)
    has_one(:dashboard, Dub.Dashboard, foreign_key: :link_id, references: :id)
    has_many(:tags, Dub.LinkTag, foreign_key: :link_id, references: :id)
    has_many(:webhooks, Dub.LinkWebhook, foreign_key: :link_id, references: :id)
    has_many(:customers, Dub.Customer, foreign_key: :link_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end