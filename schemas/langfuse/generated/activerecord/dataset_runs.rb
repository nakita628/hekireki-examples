class DatasetRuns < ApplicationRecord
  self.primary_key = %w[id project_id]

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true, uniqueness: { scope: %i[dataset_id project_id] }

  belongs_to :dataset, foreign_key: %i[dataset_id project_id], inverse_of: :dataset_runs
end
