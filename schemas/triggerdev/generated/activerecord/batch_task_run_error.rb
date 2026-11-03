class BatchTaskRunError < ApplicationRecord
  self.table_name = "BatchTaskRunError"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :index, presence: true, uniqueness: { scope: :batchTaskRunId }
  validates :taskIdentifier, presence: true
  validates :error, presence: true

  belongs_to :batch_task_run, foreign_key: "batchTaskRunId", inverse_of: :errors
end
