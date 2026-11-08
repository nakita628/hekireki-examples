class Workflow < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :enabled, default: false
  attribute :allowReentry, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :triggerType, { event: "EVENT", manual: "MANUAL", schedule: "SCHEDULE" }, validate: true

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :workflows
  has_many :steps, class_name: "WorkflowStep", foreign_key: "workflowId", inverse_of: :workflow, dependent: :destroy
  has_many :executions, class_name: "WorkflowExecution", foreign_key: "workflowId", inverse_of: :workflow,
                        dependent: :destroy
  has_many :throttle_claims, foreign_key: "workflowId", inverse_of: :workflow, dependent: :destroy
end
