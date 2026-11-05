class ChatCompaction < ApplicationRecord
  self.table_name = "ChatCompaction"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :summary, presence: true
  validates :messageCount, presence: true
  validates :compactedBeforeCreatedAt, presence: true

  belongs_to :chat, foreign_key: "chatId", inverse_of: :compactions
end
