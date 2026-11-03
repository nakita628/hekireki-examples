defmodule Papermark.Team do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t() | nil,
          plan: String.t(),
          stripe_id: String.t() | nil,
          subscription_id: String.t() | nil,
          starts_at: DateTime.t() | nil,
          ends_at: DateTime.t() | nil,
          paused_at: DateTime.t() | nil,
          pause_starts_at: DateTime.t() | nil,
          pause_ends_at: DateTime.t() | nil,
          cancelled_at: DateTime.t() | nil,
          trial_started_at: DateTime.t() | nil,
          trial_ends_at: DateTime.t() | nil,
          trial_reminder_run_id: String.t() | nil,
          trial_expired_run_id: String.t() | nil,
          limits: map() | nil,
          enable_excel_advanced_mode: boolean(),
          replicate_dataroom_folders: boolean(),
          timezone: String.t(),
          sso_enabled: boolean(),
          sso_email_domain: String.t() | nil,
          sso_enforced_at: DateTime.t() | nil,
          survey_data: map() | nil,
          agents_enabled: boolean(),
          vector_store_id: String.t() | nil,
          ignored_domains: [String.t()],
          global_block_list: [String.t()],
          referral_track: atom() | nil,
          referral_track_selected_at: DateTime.t() | nil,
          referral_profile: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          default_brand: Papermark.Brand.t() | nil,
          users: [Papermark.UserTeam.t()],
          user_datarooms: [Papermark.UserDataroom.t()],
          documents: [Papermark.Document.t()],
          folders: [Papermark.Folder.t()],
          domains: [Papermark.Domain.t()],
          invitations: [Papermark.Invitation.t()],
          sent_emails: [Papermark.SentEmail.t()],
          brands: [Papermark.Brand.t()],
          datarooms: [Papermark.Dataroom.t()],
          agreements: [Papermark.Agreement.t()],
          viewer_groups: [Papermark.ViewerGroup.t()],
          visitor_groups: [Papermark.VisitorGroup.t()],
          viewers: [Papermark.Viewer.t()],
          permission_groups: [Papermark.PermissionGroup.t()],
          link_presets: [Papermark.LinkPreset.t()],
          incoming_webhooks: [Papermark.IncomingWebhook.t()],
          restricted_tokens: [Papermark.RestrictedToken.t()],
          webhooks: [Papermark.Webhook.t()],
          conversations: [Papermark.Conversation.t()],
          dataroom_questions: [Papermark.DataroomQuestion.t()],
          dataroom_faq_items: [Papermark.DataroomFaqItem.t()],
          uploaded_documents: [Papermark.DocumentUpload.t()],
          tag: [Papermark.Tag.t()],
          annotations: [Papermark.DocumentAnnotation.t()],
          redaction_jobs: [Papermark.DocumentRedactionJob.t()],
          installed_integrations: [Papermark.InstalledIntegration.t()],
          links: [Papermark.Link.t()],
          views: [Papermark.View.t()],
          workflows: [Papermark.Workflow.t()],
          task_lists: [Papermark.TaskList.t()],
          tasks: [Papermark.Task.t()],
          chats: [Papermark.Chat.t()],
          notification_preferences: [Papermark.NotificationPreference.t()],
          referral_invites: [Papermark.ReferralInvite.t()]
        }

  schema "Team" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:plan, :string, default: "free")
    field(:stripe_id, :string, source: :stripeId)
    field(:subscription_id, :string, source: :subscriptionId)
    field(:starts_at, Papermark.PrismaDateTime, source: :startsAt)
    field(:ends_at, Papermark.PrismaDateTime, source: :endsAt)
    field(:paused_at, Papermark.PrismaDateTime, source: :pausedAt)
    field(:pause_starts_at, Papermark.PrismaDateTime, source: :pauseStartsAt)
    field(:pause_ends_at, Papermark.PrismaDateTime, source: :pauseEndsAt)
    field(:cancelled_at, Papermark.PrismaDateTime, source: :cancelledAt)
    field(:trial_started_at, Papermark.PrismaDateTime, source: :trialStartedAt)
    field(:trial_ends_at, Papermark.PrismaDateTime, source: :trialEndsAt)
    field(:trial_reminder_run_id, :string, source: :trialReminderRunId)
    field(:trial_expired_run_id, :string, source: :trialExpiredRunId)
    field(:limits, :map)
    field(:enable_excel_advanced_mode, :boolean, default: false, source: :enableExcelAdvancedMode)
    field(:replicate_dataroom_folders, :boolean, default: true, source: :replicateDataroomFolders)
    field(:timezone, :string, default: "Etc/UTC")
    field(:sso_enabled, :boolean, default: false, source: :ssoEnabled)
    field(:sso_email_domain, :string, source: :ssoEmailDomain)
    field(:sso_enforced_at, Papermark.PrismaDateTime, source: :ssoEnforcedAt)
    field(:survey_data, :map, source: :surveyData)
    field(:agents_enabled, :boolean, default: false, source: :agentsEnabled)
    field(:vector_store_id, :string, source: :vectorStoreId)
    field(:ignored_domains, {:array, :string}, source: :ignoredDomains)
    field(:global_block_list, {:array, :string}, source: :globalBlockList)
    field(:referral_track, Ecto.Enum, values: [:AFFILIATE, :PARTNER_DISCOUNT], source: :referralTrack)
    field(:referral_track_selected_at, Papermark.PrismaDateTime, source: :referralTrackSelectedAt)
    field(:referral_profile, :map, source: :referralProfile)
    field(:default_brand_id, :string, source: :defaultBrandId)
    belongs_to(:default_brand, Papermark.Brand, foreign_key: :default_brand_id, define_field: false, type: :string)
    has_many(:users, Papermark.UserTeam, foreign_key: :team_id)
    has_many(:user_datarooms, Papermark.UserDataroom, foreign_key: :team_id)
    has_many(:documents, Papermark.Document, foreign_key: :team_id)
    has_many(:folders, Papermark.Folder, foreign_key: :team_id)
    has_many(:domains, Papermark.Domain, foreign_key: :team_id)
    has_many(:invitations, Papermark.Invitation, foreign_key: :team_id)
    has_many(:sent_emails, Papermark.SentEmail, foreign_key: :team_id)
    has_many(:brands, Papermark.Brand, foreign_key: :team_id)
    has_many(:datarooms, Papermark.Dataroom, foreign_key: :team_id)
    has_many(:agreements, Papermark.Agreement, foreign_key: :team_id)
    has_many(:viewer_groups, Papermark.ViewerGroup, foreign_key: :team_id)
    has_many(:visitor_groups, Papermark.VisitorGroup, foreign_key: :team_id)
    has_many(:viewers, Papermark.Viewer, foreign_key: :team_id)
    has_many(:permission_groups, Papermark.PermissionGroup, foreign_key: :team_id)
    has_many(:link_presets, Papermark.LinkPreset, foreign_key: :team_id)
    has_many(:incoming_webhooks, Papermark.IncomingWebhook, foreign_key: :team_id)
    has_many(:restricted_tokens, Papermark.RestrictedToken, foreign_key: :team_id)
    has_many(:webhooks, Papermark.Webhook, foreign_key: :team_id)
    has_many(:conversations, Papermark.Conversation, foreign_key: :team_id)
    has_many(:dataroom_questions, Papermark.DataroomQuestion, foreign_key: :team_id)
    has_many(:dataroom_faq_items, Papermark.DataroomFaqItem, foreign_key: :team_id)
    has_many(:uploaded_documents, Papermark.DocumentUpload, foreign_key: :team_id)
    has_many(:tag, Papermark.Tag, foreign_key: :team_id)
    has_many(:annotations, Papermark.DocumentAnnotation, foreign_key: :team_id)
    has_many(:redaction_jobs, Papermark.DocumentRedactionJob, foreign_key: :team_id)
    has_many(:installed_integrations, Papermark.InstalledIntegration, foreign_key: :team_id)
    has_many(:links, Papermark.Link, foreign_key: :team_id)
    has_many(:views, Papermark.View, foreign_key: :team_id)
    has_many(:workflows, Papermark.Workflow, foreign_key: :team_id)
    has_many(:task_lists, Papermark.TaskList, foreign_key: :team_id)
    has_many(:tasks, Papermark.Task, foreign_key: :team_id)
    has_many(:chats, Papermark.Chat, foreign_key: :team_id)
    has_many(:notification_preferences, Papermark.NotificationPreference, foreign_key: :team_id)
    has_many(:referral_invites, Papermark.ReferralInvite, foreign_key: :team_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end