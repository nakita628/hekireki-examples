class WorkflowVersion < ApplicationRecord
  self.table_name = "WorkflowVersion"

  attribute :id, default: -> { Cuid2.call }

  validates :version, presence: true, uniqueness: { scope: :workflowId }
  validates :definition, exclusion: { in: [ nil ] }

  belongs_to :workflow, foreign_key: [ :workflowId, :workspaceId ], primary_key: [ :id, :workspaceId ],
                        inverse_of: :versions
  belongs_to :publisher, class_name: "User", foreign_key: "publishedBy", optional: true,
                         inverse_of: :workflow_versions_published
  has_many :runs, class_name: "WorkflowRun", foreign_key: "workflowVersionId", inverse_of: :workflow_version,
                  dependent: :nullify
end
