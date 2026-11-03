class PlatformNotification < ApplicationRecord
  self.table_name = "PlatformNotification"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :friendlyId, default: -> { Cuid.generate }
  attribute :priority, default: 0
  attribute :isDraft, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :surface, { webapp: "WEBAPP", cli: "CLI" }, validate: true
  enum :scope, { user: "USER", project: "PROJECT", organization: "ORGANIZATION", global: "GLOBAL" }, validate: true

  validates :friendlyId, uniqueness: true
  validates :title, presence: true
  validates :payload, exclusion: { in: [ nil ] }
  validates :startsAt, presence: true
  validates :endsAt, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :platform_notifications
  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :platform_notifications
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :platform_notifications
  has_many :interactions, class_name: "PlatformNotificationInteraction", foreign_key: "notificationId",
                          inverse_of: :notification, dependent: :destroy
end
