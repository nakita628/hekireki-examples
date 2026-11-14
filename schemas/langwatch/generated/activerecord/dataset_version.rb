class DatasetVersion < ApplicationRecord
  self.table_name = "DatasetVersion"

  alias_attribute :created_at, :createdAt

  validates :projectId, presence: true
  validates :datasetId, presence: true, uniqueness: { scope: :version }
  validates :version, presence: true
  validates :trigger, presence: true
  validates :contentHash, presence: true
  validates :columnTypes, exclusion: { in: [ nil ] }
  validates :rowCount, presence: true
  validates :sizeBytes, presence: true
  validates :chunkCount, presence: true
end
