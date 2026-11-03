class User < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :banned, default: false
  attribute :anonymous, default: false

  enum :time_format, { hours12: "hours12", hours24: "hours24" }, validate: { allow_nil: true }
  enum :role, { admin: "admin", user: "user" }, default: :user, validate: true

  validates :name, presence: true
  validates :email, presence: true, uniqueness: true

  belongs_to :default_destination_calendar, class_name: "ProviderCalendar", optional: true,
                                            inverse_of: :user_default_destination
  has_one :notification_preferences, class_name: "UserNotificationPreferences", inverse_of: :user, dependent: :destroy
  has_many :comments, dependent: :destroy
  has_many :polls, dependent: :destroy
  has_many :queued_emails, dependent: :destroy
  has_many :accounts, dependent: :destroy
  has_many :participants, dependent: :nullify
  has_many :payment_methods, dependent: :destroy
  has_many :space_member_invites, foreign_key: "inviter_id", inverse_of: :invited_by, dependent: :destroy
  has_many :nonprofit_applications, dependent: :destroy
  has_many :subscriptions, dependent: :destroy
  has_many :spaces, foreign_key: "owner_id", inverse_of: :owner, dependent: :destroy
  has_many :member_of, class_name: "SpaceMember", inverse_of: :user, dependent: :destroy
  has_many :scheduled_events, dependent: :destroy
  has_many :scheduled_event_invites, foreign_key: "invitee_id", inverse_of: :user, dependent: :nullify
  has_many :hosted_event_types, class_name: "EventType", foreign_key: "host_id", inverse_of: :host, dependent: :destroy
  has_many :hosted_sheets, class_name: "Sheet", foreign_key: "host_id", inverse_of: :host, dependent: :destroy
  has_many :calendar_connections, dependent: :destroy
  has_many :conferencing_connections, dependent: :destroy
  has_many :credentials, dependent: :destroy
  has_many :sessions, dependent: :destroy
  has_many :impersonated_sessions, class_name: "Session", foreign_key: "impersonated_by",
                                   inverse_of: :impersonated_by_user, dependent: :nullify
end
