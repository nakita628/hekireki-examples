defmodule Papermark.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          email_verified: DateTime.t() | nil,
          image: String.t() | nil,
          contact_id: String.t() | nil,
          plan: String.t(),
          stripe_id: String.t() | nil,
          subscription_id: String.t() | nil,
          starts_at: DateTime.t() | nil,
          ends_at: DateTime.t() | nil,
          referral_track: atom() | nil,
          referral_track_selected_at: DateTime.t() | nil,
          referral_profile: map() | nil,
          inserted_at: DateTime.t(),
          accounts: [Papermark.Account.t()],
          sessions: [Papermark.Session.t()],
          documents: [Papermark.Document.t()],
          teams: [Papermark.UserTeam.t()],
          user_datarooms: [Papermark.UserDataroom.t()],
          domains: [Papermark.Domain.t()],
          chats: [Papermark.Chat.t()],
          restricted_tokens: [Papermark.RestrictedToken.t()],
          participated_conversations: [Papermark.ConversationParticipant.t()],
          messages: [Papermark.Message.t()],
          created_dataroom_questions: [Papermark.DataroomQuestion.t()],
          published_faq_items: [Papermark.DataroomFaqItem.t()],
          created_annotations: [Papermark.DocumentAnnotation.t()],
          created_redaction_jobs: [Papermark.DocumentRedactionJob.t()],
          installed_integrations: [Papermark.InstalledIntegration.t()],
          links: [Papermark.Link.t()],
          frozen_datarooms: [Papermark.Dataroom.t()],
          created_tasks: [Papermark.Task.t()],
          task_activities: [Papermark.TaskActivity.t()],
          notification_preferences: [Papermark.NotificationPreference.t()],
          referral_invites: [Papermark.ReferralInvite.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Papermark.PrismaDateTime, source: :emailVerified)
    field(:image, :string)
    field(:contact_id, :string, source: :contactId)
    field(:plan, :string, default: "free")
    field(:stripe_id, :string, source: :stripeId)
    field(:subscription_id, :string, source: :subscriptionId)
    field(:starts_at, Papermark.PrismaDateTime, source: :startsAt)
    field(:ends_at, Papermark.PrismaDateTime, source: :endsAt)
    field(:referral_track, Ecto.Enum, values: [:AFFILIATE, :PARTNER_DISCOUNT], source: :referralTrack)
    field(:referral_track_selected_at, Papermark.PrismaDateTime, source: :referralTrackSelectedAt)
    field(:referral_profile, :map, source: :referralProfile)
    has_many(:accounts, Papermark.Account, foreign_key: :user_id)
    has_many(:sessions, Papermark.Session, foreign_key: :user_id)
    has_many(:documents, Papermark.Document, foreign_key: :owner_id)
    has_many(:teams, Papermark.UserTeam, foreign_key: :user_id)
    has_many(:user_datarooms, Papermark.UserDataroom, foreign_key: :user_id)
    has_many(:domains, Papermark.Domain, foreign_key: :user_id)
    has_many(:chats, Papermark.Chat, foreign_key: :user_id)
    has_many(:restricted_tokens, Papermark.RestrictedToken, foreign_key: :user_id)
    has_many(:participated_conversations, Papermark.ConversationParticipant, foreign_key: :user_id)
    has_many(:messages, Papermark.Message, foreign_key: :user_id)
    has_many(:created_dataroom_questions, Papermark.DataroomQuestion, foreign_key: :created_by_user_id)
    has_many(:published_faq_items, Papermark.DataroomFaqItem, foreign_key: :published_by_user_id)
    has_many(:created_annotations, Papermark.DocumentAnnotation, foreign_key: :created_by_id)
    has_many(:created_redaction_jobs, Papermark.DocumentRedactionJob, foreign_key: :created_by_id)
    has_many(:installed_integrations, Papermark.InstalledIntegration, foreign_key: :user_id)
    has_many(:links, Papermark.Link, foreign_key: :owner_id)
    has_many(:frozen_datarooms, Papermark.Dataroom, foreign_key: :frozen_by)
    has_many(:created_tasks, Papermark.Task, foreign_key: :created_by_user_id)
    has_many(:task_activities, Papermark.TaskActivity, foreign_key: :user_id)
    has_many(:notification_preferences, Papermark.NotificationPreference, foreign_key: :user_id)
    has_many(:referral_invites, Papermark.ReferralInvite, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end