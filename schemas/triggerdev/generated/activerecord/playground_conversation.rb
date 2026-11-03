class PlaygroundConversation < ApplicationRecord
  self.table_name = "PlaygroundConversation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :title, default: "New conversation"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :chatId, presence: true, uniqueness: { scope: :runtimeEnvironmentId }
  validates :agentSlug, presence: true
  validates :userId, presence: true

  belongs_to :run, class_name: "TaskRun", foreign_key: "runId", optional: true, inverse_of: :playground_conversations
  belongs_to :project, foreign_key: "projectId", inverse_of: :playground_conversations
  belongs_to :runtime_environment, foreign_key: "runtimeEnvironmentId", inverse_of: :playground_conversations
end
