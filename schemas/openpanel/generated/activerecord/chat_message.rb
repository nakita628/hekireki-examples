class ChatMessage < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :role, presence: true
  validates :parts, exclusion: { in: [ nil ] }

  belongs_to :conversation, foreign_key: "conversationId", inverse_of: :messages
end
