class DraftSendLog < ApplicationRecord
  self.table_name = "DraftSendLog"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :replyMemoryAttemptCount, default: 0
  alias_attribute :created_at, :createdAt

  validates :executedActionId, uniqueness: true
  validates :sentMessageId, presence: true
  validates :similarityScore, presence: true

  belongs_to :executed_action, foreign_key: "executedActionId", inverse_of: :draft_send_log
  has_many :reply_memory_sources, foreign_key: "draftSendLogId", inverse_of: :draft_send_log, dependent: :destroy
end
