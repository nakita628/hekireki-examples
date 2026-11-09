class ReprocessingWorkers < ApplicationRecord
  self.primary_key = %w[job_id worker_index]

  attribute :current_line, default: 0
  attribute :assigned_files, default: 0
  attribute :processed_files, default: 0
  attribute :total_lines, default: 0
  attribute :success_count, default: 0
  attribute :error_count, default: 0
  attribute :skipped_count, default: 0
  attribute :total_bytes, default: 0
  attribute :processed_bytes, default: 0

  validates :job_id, length: { maximum: 64 }
  validates :status, presence: true, length: { maximum: 32 }

  belongs_to :job, class_name: "ReprocessingJobs", inverse_of: :reprocessing_workers
end
