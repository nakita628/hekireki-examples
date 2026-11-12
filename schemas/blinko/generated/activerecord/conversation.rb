class Conversation < ApplicationRecord
  self.table_name = "conversation"

  attribute :title, default: ""
  attribute :isShare, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, length: { maximum: 255 }

  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", inverse_of: :conversations
  has_many :messages, foreign_key: "conversationId", inverse_of: :conversation, dependent: :restrict_with_error
end
