class Agent < ApplicationRecord
  self.table_name = "Agent"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :type, presence: true
  validates :config, exclusion: { in: [ nil ] }
  validates :identityKey, uniqueness: { scope: :projectId, allow_nil: true }

  belongs_to :project, foreign_key: "projectId", inverse_of: :agents
  belongs_to :workflow, foreign_key: "workflowId", optional: true, inverse_of: :agents
  belongs_to :copied_from, class_name: "Agent", foreign_key: "copiedFromAgentId", optional: true,
                           inverse_of: :copied_agents
  has_many :copied_agents, class_name: "Agent", foreign_key: "copiedFromAgentId", inverse_of: :copied_from,
                           dependent: :restrict_with_error
end
