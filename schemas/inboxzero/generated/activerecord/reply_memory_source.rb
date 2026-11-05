class ReplyMemorySource < ApplicationRecord
  self.table_name = "ReplyMemorySource"
  self.primary_key = %w[replyMemoryId draftSendLogId]

  alias_attribute :created_at, :createdAt

  belongs_to :reply_memory, foreign_key: "replyMemoryId", inverse_of: :sources
  belongs_to :draft_send_log, foreign_key: "draftSendLogId", inverse_of: :reply_memory_sources
end
