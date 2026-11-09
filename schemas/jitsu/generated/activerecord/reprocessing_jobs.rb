class ReprocessingJobs < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :total_files, default: 0
  attribute :total_workers, default: 0

  validates :id, length: { maximum: 64 }
  validates :config, exclusion: { in: [ nil ] }
  validates :status, presence: true, length: { maximum: 32 }
  validates :k8s_job_name, length: { maximum: 255 }

  has_many :reprocessing_workers, class_name: "ReprocessingWorkers", foreign_key: "job_id", inverse_of: :job,
                                  dependent: :destroy
end
