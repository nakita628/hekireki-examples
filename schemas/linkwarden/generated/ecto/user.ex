defmodule Linkwarden.User do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          name: String.t() | nil,
          username: String.t() | nil,
          email: String.t() | nil,
          email_verified: DateTime.t() | nil,
          unverified_new_email: String.t() | nil,
          image: String.t() | nil,
          password: String.t() | nil,
          locale: String.t(),
          collection_order: [integer()],
          links_route_to: atom(),
          ai_tagging_method: atom(),
          ai_predefined_tags: [String.t()],
          ai_tag_existing_links: boolean(),
          theme: atom(),
          dismissed_announcement_id: String.t() | nil,
          readable_font_family: String.t() | nil,
          readable_font_size: String.t() | nil,
          readable_line_height: String.t() | nil,
          readable_line_width: String.t() | nil,
          prevent_duplicate_links: boolean(),
          archive_as_screenshot: boolean(),
          archive_as_monolith: boolean(),
          archive_as_pdf: boolean(),
          archive_as_readable: boolean(),
          archive_as_wayback_machine: boolean(),
          referred_by: String.t() | nil,
          last_picked_at: DateTime.t() | nil,
          accept_promotional_emails: boolean(),
          trial_end_email_sent: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_subscription: Linkwarden.Subscription.t() | nil,
          subscriptions: Linkwarden.Subscription.t() | nil,
          accounts: [Linkwarden.Account.t()],
          collections: [Linkwarden.Collection.t()],
          tags: [Linkwarden.Tag.t()],
          created_links: [Linkwarden.Link.t()],
          created_collections: [Linkwarden.Collection.t()],
          highlights: [Linkwarden.Highlight.t()],
          collections_joined: [Linkwarden.UsersAndCollections.t()],
          access_tokens: [Linkwarden.AccessToken.t()],
          dashboard_sections: [Linkwarden.DashboardSection.t()],
          pinned_links: [Linkwarden.Link.t()]
        }

  schema "User" do
    field(:uuid, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:name, :string)
    field(:username, :string)
    field(:email, :string)
    field(:email_verified, Linkwarden.PrismaDateTime, source: :emailVerified)
    field(:unverified_new_email, :string, source: :unverifiedNewEmail)
    field(:image, :string)
    field(:password, :string)
    field(:locale, :string, default: "en")
    field(:collection_order, {:array, :integer}, source: :collectionOrder)
    field(:links_route_to, Ecto.Enum, values: [:ORIGINAL, :PDF, :READABLE, :MONOLITH, :SCREENSHOT, :DETAILS], default: :ORIGINAL, source: :linksRouteTo)
    field(:ai_tagging_method, Ecto.Enum, values: [:DISABLED, :GENERATE, :EXISTING, :PREDEFINED], default: :DISABLED, source: :aiTaggingMethod)
    field(:ai_predefined_tags, {:array, :string}, source: :aiPredefinedTags)
    field(:ai_tag_existing_links, :boolean, default: false, source: :aiTagExistingLinks)
    field(:theme, Ecto.Enum, values: [:dark, :light, :auto], default: :dark)
    field(:dismissed_announcement_id, :string, source: :dismissedAnnouncementId)
    field(:readable_font_family, :string, default: "sans-serif", source: :readableFontFamily)
    field(:readable_font_size, :string, default: "20px", source: :readableFontSize)
    field(:readable_line_height, :string, default: "1.8", source: :readableLineHeight)
    field(:readable_line_width, :string, default: "normal", source: :readableLineWidth)
    field(:prevent_duplicate_links, :boolean, default: false, source: :preventDuplicateLinks)
    field(:archive_as_screenshot, :boolean, default: true, source: :archiveAsScreenshot)
    field(:archive_as_monolith, :boolean, default: true, source: :archiveAsMonolith)
    field(:archive_as_pdf, :boolean, default: true, source: :archiveAsPDF)
    field(:archive_as_readable, :boolean, default: true, source: :archiveAsReadable)
    field(:archive_as_wayback_machine, :boolean, default: false, source: :archiveAsWaybackMachine)
    field(:referred_by, :string, source: :referredBy)
    field(:last_picked_at, Linkwarden.PrismaDateTime, source: :lastPickedAt)
    field(:accept_promotional_emails, :boolean, default: false, source: :acceptPromotionalEmails)
    field(:trial_end_email_sent, :boolean, default: false, source: :trialEndEmailSent)
    field(:parent_subscription_id, :id, source: :parentSubscriptionId)
    belongs_to(:parent_subscription, Linkwarden.Subscription, foreign_key: :parent_subscription_id, define_field: false)
    has_one(:subscriptions, Linkwarden.Subscription, foreign_key: :user_id)
    has_many(:accounts, Linkwarden.Account, foreign_key: :user_id)
    has_many(:collections, Linkwarden.Collection, foreign_key: :owner_id)
    has_many(:tags, Linkwarden.Tag, foreign_key: :owner_id)
    has_many(:created_links, Linkwarden.Link, foreign_key: :created_by_id)
    has_many(:created_collections, Linkwarden.Collection, foreign_key: :created_by_id)
    has_many(:highlights, Linkwarden.Highlight, foreign_key: :user_id)
    has_many(:collections_joined, Linkwarden.UsersAndCollections, foreign_key: :user_id)
    has_many(:access_tokens, Linkwarden.AccessToken, foreign_key: :user_id)
    has_many(:dashboard_sections, Linkwarden.DashboardSection, foreign_key: :user_id)
    many_to_many(:pinned_links, Linkwarden.Link, join_through: "_PinnedLinks", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end