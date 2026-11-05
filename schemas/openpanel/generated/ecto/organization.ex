defmodule OpenPanel.Organization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t() | nil,
          timezone: String.t() | nil,
          onboarding: String.t() | nil,
          attribution: map() | nil,
          subscription_id: String.t() | nil,
          subscription_customer_id: String.t() | nil,
          subscription_price_id: String.t() | nil,
          subscription_product_id: String.t() | nil,
          subscription_status: String.t() | nil,
          subscription_starts_at: DateTime.t() | nil,
          subscription_ends_at: DateTime.t() | nil,
          subscription_canceled_at: DateTime.t() | nil,
          subscription_period_events_count: integer(),
          subscription_period_events_count_exceeded_at: DateTime.t() | nil,
          subscription_period_events_limit: integer(),
          subscription_interval: String.t() | nil,
          subscription_cancel_reason: String.t() | nil,
          subscription_cancel_comment: String.t() | nil,
          subscription_save_discount_applied_at: DateTime.t() | nil,
          subscription_pause_at_period_end: boolean(),
          subscription_resumes_at: DateTime.t() | nil,
          subscription_first_started_at: DateTime.t() | nil,
          usage_warning_sent_at: DateTime.t() | nil,
          usage_exceeded_sent_at: DateTime.t() | nil,
          subscription_discount: map() | nil,
          wind_down_started_at: DateTime.t() | nil,
          wind_down_step: String.t() | nil,
          delete_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by: OpenPanel.User.t() | nil,
          subscription_created_by: OpenPanel.User.t() | nil,
          projects: [OpenPanel.Project.t()],
          members: [OpenPanel.Member.t()],
          client: [OpenPanel.Client.t()],
          dashboard: [OpenPanel.Dashboard.t()],
          share_overview: [OpenPanel.ShareOverview.t()],
          share_dashboard: [OpenPanel.ShareDashboard.t()],
          share_report: [OpenPanel.ShareReport.t()],
          share_widget: [OpenPanel.ShareWidget.t()],
          integrations: [OpenPanel.Integration.t()],
          invites: [OpenPanel.Invite.t()],
          conversations: [OpenPanel.Conversation.t()]
        }

  schema "organizations" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:slug, :string)
    field(:timezone, :string)
    field(:onboarding, :string, default: "completed")
    field(:attribution, :map)
    field(:subscription_id, :string, source: :subscriptionId)
    field(:subscription_customer_id, :string, source: :subscriptionCustomerId)
    field(:subscription_price_id, :string, source: :subscriptionPriceId)
    field(:subscription_product_id, :string, source: :subscriptionProductId)
    field(:subscription_status, :string, source: :subscriptionStatus)
    field(:subscription_starts_at, OpenPanel.PrismaDateTime, source: :subscriptionStartsAt)
    field(:subscription_ends_at, OpenPanel.PrismaDateTime, source: :subscriptionEndsAt)
    field(:subscription_canceled_at, OpenPanel.PrismaDateTime, source: :subscriptionCanceledAt)
    field(:subscription_period_events_count, :integer, default: 0, source: :subscriptionPeriodEventsCount)
    field(:subscription_period_events_count_exceeded_at, OpenPanel.PrismaDateTime, source: :subscriptionPeriodEventsCountExceededAt)
    field(:subscription_period_events_limit, :integer, default: 0, source: :subscriptionPeriodEventsLimit)
    field(:subscription_interval, :string, source: :subscriptionInterval)
    field(:subscription_cancel_reason, :string, source: :subscriptionCancelReason)
    field(:subscription_cancel_comment, :string, source: :subscriptionCancelComment)
    field(:subscription_save_discount_applied_at, OpenPanel.PrismaDateTime, source: :subscriptionSaveDiscountAppliedAt)
    field(:subscription_pause_at_period_end, :boolean, default: false, source: :subscriptionPauseAtPeriodEnd)
    field(:subscription_resumes_at, OpenPanel.PrismaDateTime, source: :subscriptionResumesAt)
    field(:subscription_first_started_at, OpenPanel.PrismaDateTime, source: :subscriptionFirstStartedAt)
    field(:usage_warning_sent_at, OpenPanel.PrismaDateTime, source: :usageWarningSentAt)
    field(:usage_exceeded_sent_at, OpenPanel.PrismaDateTime, source: :usageExceededSentAt)
    field(:subscription_discount, :map, source: :subscriptionDiscount)
    field(:wind_down_started_at, OpenPanel.PrismaDateTime, source: :windDownStartedAt)
    field(:wind_down_step, :string, source: :windDownStep)
    field(:delete_at, OpenPanel.PrismaDateTime, source: :deleteAt)
    field(:created_by_user_id, :string, source: :createdByUserId)
    field(:subscription_created_by_user_id, :string, source: :subscriptionCreatedByUserId)
    belongs_to(:created_by, OpenPanel.User, foreign_key: :created_by_user_id, define_field: false, type: :string)
    belongs_to(:subscription_created_by, OpenPanel.User, foreign_key: :subscription_created_by_user_id, define_field: false, type: :string)
    has_many(:projects, OpenPanel.Project, foreign_key: :organization_id, references: :id)
    has_many(:members, OpenPanel.Member, foreign_key: :organization_id, references: :id)
    has_many(:client, OpenPanel.Client, foreign_key: :organization_id, references: :id)
    has_many(:dashboard, OpenPanel.Dashboard, foreign_key: :organization_id, references: :id)
    has_many(:share_overview, OpenPanel.ShareOverview, foreign_key: :organization_id, references: :id)
    has_many(:share_dashboard, OpenPanel.ShareDashboard, foreign_key: :organization_id, references: :id)
    has_many(:share_report, OpenPanel.ShareReport, foreign_key: :organization_id, references: :id)
    has_many(:share_widget, OpenPanel.ShareWidget, foreign_key: :organization_id, references: :id)
    has_many(:integrations, OpenPanel.Integration, foreign_key: :organization_id, references: :id)
    has_many(:invites, OpenPanel.Invite, foreign_key: :organization_id, references: :id)
    has_many(:conversations, OpenPanel.Conversation, foreign_key: :organization_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end