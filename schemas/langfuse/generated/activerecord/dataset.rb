class Dataset < ApplicationRecord
  self.primary_key = %w[id project_id]

  attribute :id, default: -> { Cuid.generate }
  attribute :remote_experiment_enabled, default: true

  validates :name, presence: true, uniqueness: { scope: :project_id }

  belongs_to :project, inverse_of: :dataset
  has_many :dataset_items, foreign_key: %i[dataset_id project_id], inverse_of: :dataset, dependent: :destroy
  has_many :dataset_runs, class_name: "DatasetRuns", foreign_key: %i[dataset_id project_id], inverse_of: :dataset,
                          dependent: :destroy
end
