class BackgroundJobTask < ApplicationRecord
  self.table_name = "BackgroundJobTask"

  attribute :retried, default: 0
  attribute :maxRetries, default: 3
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", completed: "COMPLETED", failed: "FAILED" }, default: :pending, validate: true

  validates :name, presence: true

  belongs_to :background_job, foreign_key: "jobId", inverse_of: :tasks
end
