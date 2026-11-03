class WorkflowRun < ApplicationRecord
  self.table_name = "WorkflowRun"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid2.call }
  attribute :isDryRun, default: false
  attribute :attempt, default: 0
  attribute :data, default: -> { JSON.parse("{}") }

  enum :status, { queued: "queued", running: "running", completed: "completed", failed: "failed", canceled: "canceled" },
       default: :queued, validate: true

  validates :triggerType, presence: true
  validates :idempotencyKey, uniqueness: { scope: :workflowId, allow_nil: true }
  validates :triggerPayload, exclusion: { in: [ nil ] }

  belongs_to :workflow, foreign_key: [ :workflowId, :workspaceId ], primary_key: [ :id, :workspaceId ],
                        inverse_of: :runs
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :workflow_runs
  belongs_to :workflow_version, foreign_key: "workflowVersionId", optional: true, inverse_of: :runs
  belongs_to :response, foreign_key: "responseId", optional: true, inverse_of: :workflow_runs
  has_many :logs, class_name: "WorkflowRunLog", foreign_key: "runId", inverse_of: :run, dependent: :destroy
end
