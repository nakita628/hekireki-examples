class MastraDatasetVersions < ApplicationRecord
  alias_attribute :created_at, :createdAt

  validates :datasetId, presence: true, uniqueness: { scope: :version }
  validates :version, presence: true
end
