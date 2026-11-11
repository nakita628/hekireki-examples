class ChatActionTotal < ApplicationRecord
  self.table_name = "ChatActionTotal"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :count, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :action, presence: true, uniqueness: { scope: :chatId }

  belongs_to :chat, foreign_key: "chatId", inverse_of: :action_totals
end
