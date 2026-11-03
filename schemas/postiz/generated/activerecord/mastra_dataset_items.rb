class MastraDatasetItems < ApplicationRecord
  self.primary_key = %w[id datasetVersion]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :datasetId, presence: true
  validates :isDeleted, inclusion: { in: [ true, false ] }
  validates :input, exclusion: { in: [ nil ] }
end
