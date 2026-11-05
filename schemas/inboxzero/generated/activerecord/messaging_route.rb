class MessagingRoute < ApplicationRecord
  self.table_name = "MessagingRoute"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :purpose, { rule_notifications: "RULE_NOTIFICATIONS", scheduled_check_ins: "SCHEDULED_CHECK_INS", meeting_briefs: "MEETING_BRIEFS", document_filings: "DOCUMENT_FILINGS", digests: "DIGESTS", follow_ups: "FOLLOW_UPS" },
       validate: true
  enum :targetType, { channel: "CHANNEL", direct_message: "DIRECT_MESSAGE" }, validate: true

  validates :targetId, presence: true
  validates :messagingChannelId, uniqueness: { scope: :purpose }

  belongs_to :messaging_channel, foreign_key: "messagingChannelId", inverse_of: :routes
end
