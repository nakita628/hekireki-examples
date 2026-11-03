class ChatMessage < ApplicationRecord
  self.table_name = "ChatMessage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :role, presence: true
  validates :content, presence: true

  belongs_to :chat, foreign_key: "chatId", inverse_of: :messages
end
