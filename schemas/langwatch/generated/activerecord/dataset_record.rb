class DatasetRecord < ApplicationRecord
  self.table_name = "DatasetRecord"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :entry, exclusion: { in: [ nil ] }

  belongs_to :dataset, foreign_key: "datasetId", inverse_of: :dataset_records
  belongs_to :project, foreign_key: "projectId", inverse_of: :dataset_records
end
