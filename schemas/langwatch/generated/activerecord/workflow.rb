class Workflow < ApplicationRecord
  self.table_name = "Workflow"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :isEvaluator, default: false
  attribute :isComponent, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :icon, presence: true
  validates :description, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :workflows
  belongs_to :latest_version, class_name: "WorkflowVersion", foreign_key: "latestVersionId", optional: true,
                              inverse_of: :workflow_as_latest_version
  belongs_to :current_version, class_name: "WorkflowVersion", foreign_key: "currentVersionId", optional: true,
                               inverse_of: :workflow_as_current_version
  belongs_to :published_by, class_name: "User", foreign_key: "publishedById", optional: true, inverse_of: :workflow
  belongs_to :copied_from, class_name: "Workflow", foreign_key: "copiedFromWorkflowId", optional: true,
                           inverse_of: :copied_workflows
  has_many :versions, class_name: "WorkflowVersion", foreign_key: "workflowId", inverse_of: :workflow,
                      dependent: :restrict_with_error
  has_many :experiments, foreign_key: "workflowId", inverse_of: :workflow, dependent: :nullify
  has_many :copied_workflows, class_name: "Workflow", foreign_key: "copiedFromWorkflowId", inverse_of: :copied_from,
                              dependent: :restrict_with_error
  has_many :agents, foreign_key: "workflowId", inverse_of: :workflow, dependent: :nullify
  has_many :evaluators, foreign_key: "workflowId", inverse_of: :workflow, dependent: :nullify
end
