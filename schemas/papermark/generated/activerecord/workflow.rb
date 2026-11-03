class Workflow < ApplicationRecord
  self.table_name = "Workflow"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isActive, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :entryLinkId, uniqueness: true

  belongs_to :entry_link, class_name: "Link", foreign_key: "entryLinkId", inverse_of: :workflow
  belongs_to :team, foreign_key: "teamId", inverse_of: :workflows
  has_many :steps, class_name: "WorkflowStep", foreign_key: "workflowId", inverse_of: :workflow, dependent: :destroy
  has_many :executions, class_name: "WorkflowExecution", foreign_key: "workflowId", inverse_of: :workflow,
                        dependent: :destroy
end
