class WorkspaceChats < ApplicationRecord
  attribute :include, default: true
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :workspaceId, presence: true
  validates :prompt, presence: true
  validates :response, presence: true

  belongs_to :users, foreign_key: "user_id", optional: true, inverse_of: :workspace_chats
end
