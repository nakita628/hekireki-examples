class Conversation < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :project, foreign_key: "projectId", inverse_of: :conversations
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :conversations
  belongs_to :user, foreign_key: "userId", inverse_of: :conversations
  has_many :messages, class_name: "ChatMessage", foreign_key: "conversationId", inverse_of: :conversation,
                      dependent: :destroy
end
