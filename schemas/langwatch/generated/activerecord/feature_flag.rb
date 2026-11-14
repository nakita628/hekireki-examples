class FeatureFlag < ApplicationRecord
  self.table_name = "FeatureFlag"
  self.primary_key = "key"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :enabled, inclusion: { in: [ true, false ] }
end
