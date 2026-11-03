class BatchTaskRunItem < ApplicationRecord
  self.table_name = "BatchTaskRunItem"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", failed: "FAILED", canceled: "CANCELED", completed: "COMPLETED" },
       default: :pending, validate: true

  validates :batchTaskRunId, uniqueness: { scope: :taskRunId }

  belongs_to :batch_task_run, foreign_key: "batchTaskRunId", inverse_of: :items
  belongs_to :task_run, foreign_key: "taskRunId", inverse_of: :batch_items
  belongs_to :task_run_attempt, foreign_key: "taskRunAttemptId", optional: true, inverse_of: :batch_task_run_items
end
