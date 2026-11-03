class WorkflowStep < ApplicationRecord
  self.table_name = "WorkflowStep"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :stepType, { router: "ROUTER" }, default: :router, validate: true

  validates :name, presence: true
  validates :stepOrder, presence: true, uniqueness: { scope: :workflowId }
  validates :conditions, exclusion: { in: [ nil ] }
  validates :actions, exclusion: { in: [ nil ] }

  belongs_to :workflow, foreign_key: "workflowId", inverse_of: :steps
  has_many :execution_logs, class_name: "WorkflowStepLog", foreign_key: "workflowStepId", inverse_of: :step,
                            dependent: :destroy
end
