class Message < ApplicationRecord
  self.table_name = "message"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true
  validates :role, presence: true, length: { maximum: 50 }

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :messages
end
