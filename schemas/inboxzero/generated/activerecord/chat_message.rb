class ChatMessage < ApplicationRecord
  self.table_name = "ChatMessage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :role, presence: true
  validates :parts, exclusion: { in: [ nil ] }

  belongs_to :chat, foreign_key: "chatId", inverse_of: :messages
end
