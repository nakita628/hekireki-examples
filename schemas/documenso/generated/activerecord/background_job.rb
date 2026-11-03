class BackgroundJob < ApplicationRecord
  self.table_name = "BackgroundJob"

  attribute :id, default: -> { Cuid.generate }
  attribute :retried, default: 0
  attribute :maxRetries, default: 3
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", processing: "PROCESSING", completed: "COMPLETED", failed: "FAILED" },
       default: :pending, validate: true

  validates :jobId, presence: true
  validates :name, presence: true
  validates :version, presence: true

  has_many :tasks, class_name: "BackgroundJobTask", foreign_key: "jobId", inverse_of: :background_job,
                   dependent: :destroy
end
