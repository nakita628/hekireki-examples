class EmbedChats < ApplicationRecord
  attribute :include, default: true
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :prompt, presence: true
  validates :response, presence: true
  validates :session_id, presence: true

  belongs_to :embed_config, class_name: "EmbedConfigs", foreign_key: "embed_id", inverse_of: :embed_chats
  belongs_to :users, foreign_key: "usersId", optional: true, inverse_of: :embed_chats
end
