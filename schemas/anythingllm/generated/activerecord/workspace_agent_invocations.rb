class WorkspaceAgentInvocations < ApplicationRecord
  attribute :closed, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :uuid, presence: true, uniqueness: true
  validates :prompt, presence: true

  belongs_to :user, class_name: "Users", optional: true, inverse_of: :workspace_agent_invocations
  belongs_to :workspace, class_name: "Workspaces", inverse_of: :workspace_agent_invocations
end
