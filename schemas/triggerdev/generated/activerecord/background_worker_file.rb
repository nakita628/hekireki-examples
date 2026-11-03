class BackgroundWorkerFile < ApplicationRecord
  self.table_name = "BackgroundWorkerFile"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :friendlyId, presence: true, uniqueness: true
  validates :filePath, presence: true
  validates :contentHash, presence: true, uniqueness: { scope: :projectId }
  validates :contents, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :background_worker_file
  has_many :tasks, class_name: "BackgroundWorkerTask", foreign_key: "fileId", inverse_of: :file, dependent: :destroy
  has_and_belongs_to_many :background_workers, join_table: "_BackgroundWorkerToBackgroundWorkerFile", foreign_key: "B",
                                               association_foreign_key: "A"
end
