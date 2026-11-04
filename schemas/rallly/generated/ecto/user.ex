defmodule Rallly.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          email: String.t(),
          email_verified: boolean() | nil,
          image: String.t() | nil,
          time_zone: String.t() | nil,
          week_start: integer() | nil,
          time_format: atom() | nil,
          locale: String.t() | nil,
          customer_id: String.t() | nil,
          banned: boolean(),
          banned_at: DateTime.t() | nil,
          ban_reason: String.t() | nil,
          ban_expires: DateTime.t() | nil,
          role: atom(),
          last_login_method: String.t() | nil,
          is_anonymous: boolean(),
          last_seen_at: DateTime.t(),
          deleted_at: DateTime.t() | nil,
          job_title: String.t() | nil,
          review_requested_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          default_destination_calendar: Rallly.ProviderCalendar.t() | nil,
          notification_preferences: Rallly.UserNotificationPreferences.t() | nil,
          comments: [Rallly.Comment.t()],
          polls: [Rallly.Poll.t()],
          queued_emails: [Rallly.QueuedEmail.t()],
          accounts: [Rallly.Account.t()],
          participants: [Rallly.Participant.t()],
          payment_methods: [Rallly.PaymentMethod.t()],
          space_member_invites: [Rallly.SpaceMemberInvite.t()],
          nonprofit_applications: [Rallly.NonprofitApplication.t()],
          subscriptions: [Rallly.Subscription.t()],
          spaces: [Rallly.Space.t()],
          member_of: [Rallly.SpaceMember.t()],
          scheduled_events: [Rallly.ScheduledEvent.t()],
          scheduled_event_invites: [Rallly.ScheduledEventInvite.t()],
          hosted_event_types: [Rallly.EventType.t()],
          hosted_sheets: [Rallly.Sheet.t()],
          calendar_connections: [Rallly.CalendarConnection.t()],
          conferencing_connections: [Rallly.ConferencingConnection.t()],
          credentials: [Rallly.Credential.t()],
          sessions: [Rallly.Session.t()],
          impersonated_sessions: [Rallly.Session.t()]
        }

  schema "users" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, :boolean)
    field(:image, :string)
    field(:time_zone, :string)
    field(:week_start, :integer)
    field(:time_format, Ecto.Enum, values: [:hours12, :hours24])
    field(:locale, :string)
    field(:customer_id, :string)
    field(:banned, :boolean, default: false)
    field(:banned_at, Rallly.PrismaDateTime)
    field(:ban_reason, :string)
    field(:ban_expires, Rallly.PrismaDateTime)
    field(:role, Ecto.Enum, values: [:admin, :user], default: :user)
    field(:last_login_method, :string)
    field(:is_anonymous, :boolean, default: false, source: :anonymous)
    field(:last_seen_at, Rallly.PrismaDateTime, autogenerate: true)
    field(:deleted_at, Rallly.PrismaDateTime)
    field(:job_title, :string)
    field(:review_requested_at, Rallly.PrismaDateTime)
    belongs_to(:default_destination_calendar, Rallly.ProviderCalendar, foreign_key: :default_destination_calendar_id, type: :string)
    has_one(:notification_preferences, Rallly.UserNotificationPreferences, foreign_key: :user_id, references: :id)
    has_many(:comments, Rallly.Comment, foreign_key: :user_id, references: :id)
    has_many(:polls, Rallly.Poll, foreign_key: :user_id, references: :id)
    has_many(:queued_emails, Rallly.QueuedEmail, foreign_key: :user_id, references: :id)
    has_many(:accounts, Rallly.Account, foreign_key: :user_id, references: :id)
    has_many(:participants, Rallly.Participant, foreign_key: :user_id, references: :id)
    has_many(:payment_methods, Rallly.PaymentMethod, foreign_key: :user_id, references: :id)
    has_many(:space_member_invites, Rallly.SpaceMemberInvite, foreign_key: :inviter_id, references: :id)
    has_many(:nonprofit_applications, Rallly.NonprofitApplication, foreign_key: :user_id, references: :id)
    has_many(:subscriptions, Rallly.Subscription, foreign_key: :user_id, references: :id)
    has_many(:spaces, Rallly.Space, foreign_key: :owner_id, references: :id)
    has_many(:member_of, Rallly.SpaceMember, foreign_key: :user_id, references: :id)
    has_many(:scheduled_events, Rallly.ScheduledEvent, foreign_key: :user_id, references: :id)
    has_many(:scheduled_event_invites, Rallly.ScheduledEventInvite, foreign_key: :invitee_id, references: :id)
    has_many(:hosted_event_types, Rallly.EventType, foreign_key: :host_id, references: :id)
    has_many(:hosted_sheets, Rallly.Sheet, foreign_key: :host_id, references: :id)
    has_many(:calendar_connections, Rallly.CalendarConnection, foreign_key: :user_id, references: :id)
    has_many(:conferencing_connections, Rallly.ConferencingConnection, foreign_key: :user_id, references: :id)
    has_many(:credentials, Rallly.Credential, foreign_key: :user_id, references: :id)
    has_many(:sessions, Rallly.Session, foreign_key: :user_id, references: :id)
    has_many(:impersonated_sessions, Rallly.Session, foreign_key: :impersonated_by, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end