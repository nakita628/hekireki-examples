class WorkflowRunLog < ApplicationRecord
  self.table_name = "WorkflowRunLog"

  attribute :id, default: -> { Cuid2.call }
  attribute :input, default: -> { JSON.parse("{}") }
  attribute :output, default: -> { JSON.parse("{}") }

  enum :status, { pending: "pending", running: "running", succeeded: "succeeded", failed: "failed", skipped: "skipped" },
       default: :pending, validate: true

  validates :sequence, presence: true
  validates :stepId, presence: true, uniqueness: { scope: :runId }
  validates :stepType, presence: true

  belongs_to :run, class_name: "WorkflowRun", foreign_key: "runId", inverse_of: :logs
end
