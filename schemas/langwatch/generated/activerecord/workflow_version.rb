class WorkflowVersion < ApplicationRecord
  self.table_name = "WorkflowVersion"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :autoSaved, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :version, presence: true, uniqueness: { scope: :workflowId }
  validates :commitMessage, presence: true
  validates :dsl, exclusion: { in: [ nil ] }

  belongs_to :author, class_name: "User", foreign_key: "authorId", inverse_of: :workflow_version
  belongs_to :project, foreign_key: "projectId", inverse_of: :workflow_version
  belongs_to :workflow, foreign_key: "workflowId", inverse_of: :versions
  belongs_to :parent, class_name: "WorkflowVersion", foreign_key: "parentId", optional: true, inverse_of: :children
  has_many :children, class_name: "WorkflowVersion", foreign_key: "parentId", inverse_of: :parent,
                      dependent: :restrict_with_error
  has_many :workflow_as_latest_version, class_name: "Workflow", foreign_key: "latestVersionId",
                                        inverse_of: :latest_version, dependent: :restrict_with_error
  has_many :workflow_as_current_version, class_name: "Workflow", foreign_key: "currentVersionId",
                                         inverse_of: :current_version, dependent: :restrict_with_error
end
