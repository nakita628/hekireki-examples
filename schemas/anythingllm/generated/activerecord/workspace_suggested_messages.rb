class WorkspaceSuggestedMessages < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :heading, presence: true
  validates :message, presence: true

  belongs_to :workspace, class_name: "Workspaces", foreign_key: "workspaceId", inverse_of: :workspace_suggested_messages
end
