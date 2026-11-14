class Dataset < ApplicationRecord
  self.table_name = "Dataset"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :useS3, default: false
  attribute :contentLayout, default: "postgres"
  attribute :status, default: "ready"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: { scope: :projectId }
  validates :columnTypes, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :datasets
  has_many :dataset_records, foreign_key: "datasetId", inverse_of: :dataset, dependent: :restrict_with_error
  has_many :batch_evaluations, foreign_key: "datasetId", inverse_of: :dataset, dependent: :restrict_with_error
end
