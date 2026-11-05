class ChatMemory < ApplicationRecord
  self.table_name = "ChatMemory"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true

  belongs_to :chat, foreign_key: "chatId", optional: true, inverse_of: :memories
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :chat_memories
end
