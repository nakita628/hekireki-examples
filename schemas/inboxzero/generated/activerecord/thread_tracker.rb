class ThreadTracker < ApplicationRecord
  self.table_name = "ThreadTracker"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :resolved, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { awaiting: "AWAITING", needs_reply: "NEEDS_REPLY", needs_action: "NEEDS_ACTION" }, validate: true

  validates :sentAt, presence: true
  validates :threadId, presence: true, uniqueness: { scope: [ :emailAccountId, :messageId ] }
  validates :messageId, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :thread_trackers
end
