defmodule Dub.Project do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          logo: String.t() | nil,
          invite_code: String.t() | nil,
          plan: String.t(),
          plan_tier: integer(),
          plan_period: atom() | nil,
          stripe_id: String.t() | nil,
          billing_cycle_start: integer(),
          billing_cycle_ends_at: DateTime.t() | nil,
          trial_ends_at: DateTime.t() | nil,
          subscription_canceled_at: DateTime.t() | nil,
          payment_failed_at: DateTime.t() | nil,
          invoice_prefix: String.t() | nil,
          stripe_connect_id: String.t() | nil,
          stripe_connect_mode: atom() | nil,
          shopify_store_id: String.t() | nil,
          total_links: integer(),
          total_clicks: integer(),
          usage: integer(),
          usage_limit: integer(),
          links_usage: integer(),
          links_limit: integer(),
          domains_limit: integer(),
          tags_limit: integer(),
          folders_usage: integer(),
          folders_limit: integer(),
          users_limit: integer(),
          ai_usage: integer(),
          ai_limit: integer(),
          store: map() | nil,
          site_visit_tracking_settings: map() | nil,
          allowed_hostnames: map() | nil,
          publishable_key: String.t() | nil,
          conversion_enabled: boolean(),
          webhook_enabled: boolean(),
          dot_link_claimed: boolean(),
          sso_email_domain: String.t() | nil,
          sso_enforced_at: DateTime.t() | nil,
          usage_last_checked: DateTime.t(),
          disabled_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          users: [Dub.ProjectUsers.t()],
          invites: [Dub.ProjectInvite.t()],
          sent_emails: [Dub.SentEmail.t()],
          links: [Dub.Link.t()],
          domains: [Dub.Domain.t()],
          tags: [Dub.Tag.t()],
          invoices: [Dub.Invoice.t()],
          customers: [Dub.Customer.t()],
          default_domains: [Dub.DefaultDomains.t()],
          restricted_tokens: [Dub.RestrictedToken.t()],
          o_auth_codes: [Dub.OAuthCode.t()],
          integrations: [Dub.Integration.t()],
          installed_integrations: [Dub.InstalledIntegration.t()],
          webhooks: [Dub.Webhook.t()],
          folders: [Dub.Folder.t()],
          registered_domains: [Dub.RegisteredDomain.t()],
          dashboards: [Dub.Dashboard.t()],
          utm_templates: [Dub.UtmTemplate.t()],
          year_in_reviews: [Dub.YearInReview.t()]
        }

  schema "Project" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:logo, :string)
    field(:invite_code, :string, source: :inviteCode)
    field(:plan, :string, default: "free")
    field(:plan_tier, :integer, default: 1, source: :planTier)
    field(:plan_period, Ecto.Enum, values: [:monthly, :yearly, :quarterly, :biweekly], source: :planPeriod)
    field(:stripe_id, :string, source: :stripeId)
    field(:billing_cycle_start, :integer, source: :billingCycleStart)
    field(:billing_cycle_ends_at, Dub.PrismaDateTime, source: :billingCycleEndsAt)
    field(:trial_ends_at, Dub.PrismaDateTime, source: :trialEndsAt)
    field(:subscription_canceled_at, Dub.PrismaDateTime, source: :subscriptionCanceledAt)
    field(:payment_failed_at, Dub.PrismaDateTime, source: :paymentFailedAt)
    field(:invoice_prefix, :string, source: :invoicePrefix)
    field(:stripe_connect_id, :string, source: :stripeConnectId)
    field(:stripe_connect_mode, Ecto.Enum, values: [:live, :test, :sandbox], source: :stripeConnectMode)
    field(:shopify_store_id, :string, source: :shopifyStoreId)
    field(:total_links, :integer, default: 0, source: :totalLinks)
    field(:total_clicks, :integer, default: 0, source: :totalClicks)
    field(:usage, :integer, default: 0)
    field(:usage_limit, :integer, default: 1000, source: :usageLimit)
    field(:links_usage, :integer, default: 0, source: :linksUsage)
    field(:links_limit, :integer, default: 25, source: :linksLimit)
    field(:domains_limit, :integer, default: 3, source: :domainsLimit)
    field(:tags_limit, :integer, default: 5, source: :tagsLimit)
    field(:folders_usage, :integer, default: 0, source: :foldersUsage)
    field(:folders_limit, :integer, default: 0, source: :foldersLimit)
    field(:users_limit, :integer, default: 1, source: :usersLimit)
    field(:ai_usage, :integer, default: 0, source: :aiUsage)
    field(:ai_limit, :integer, default: 10, source: :aiLimit)
    field(:store, :map)
    field(:site_visit_tracking_settings, :map, source: :siteVisitTrackingSettings)
    field(:allowed_hostnames, :map, source: :allowedHostnames)
    field(:publishable_key, :string, source: :publishableKey)
    field(:conversion_enabled, :boolean, default: false, source: :conversionEnabled)
    field(:webhook_enabled, :boolean, default: false, source: :webhookEnabled)
    field(:dot_link_claimed, :boolean, default: false, source: :dotLinkClaimed)
    field(:sso_email_domain, :string, source: :ssoEmailDomain)
    field(:sso_enforced_at, Dub.PrismaDateTime, source: :ssoEnforcedAt)
    field(:usage_last_checked, Dub.PrismaDateTime, autogenerate: true, source: :usageLastChecked)
    field(:disabled_at, Dub.PrismaDateTime, source: :disabledAt)
    has_many(:users, Dub.ProjectUsers, foreign_key: :project_id)
    has_many(:invites, Dub.ProjectInvite, foreign_key: :project_id)
    has_many(:sent_emails, Dub.SentEmail, foreign_key: :project_id)
    has_many(:links, Dub.Link, foreign_key: :project_id)
    has_many(:domains, Dub.Domain, foreign_key: :project_id)
    has_many(:tags, Dub.Tag, foreign_key: :project_id)
    has_many(:invoices, Dub.Invoice, foreign_key: :workspace_id)
    has_many(:customers, Dub.Customer, foreign_key: :project_id)
    has_many(:default_domains, Dub.DefaultDomains, foreign_key: :project_id)
    has_many(:restricted_tokens, Dub.RestrictedToken, foreign_key: :project_id)
    has_many(:o_auth_codes, Dub.OAuthCode, foreign_key: :project_id)
    has_many(:integrations, Dub.Integration, foreign_key: :project_id)
    has_many(:installed_integrations, Dub.InstalledIntegration, foreign_key: :project_id)
    has_many(:webhooks, Dub.Webhook, foreign_key: :project_id)
    has_many(:folders, Dub.Folder, foreign_key: :project_id)
    has_many(:registered_domains, Dub.RegisteredDomain, foreign_key: :project_id)
    has_many(:dashboards, Dub.Dashboard, foreign_key: :project_id)
    has_many(:utm_templates, Dub.UtmTemplate, foreign_key: :project_id)
    has_many(:year_in_reviews, Dub.YearInReview, foreign_key: :workspace_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end