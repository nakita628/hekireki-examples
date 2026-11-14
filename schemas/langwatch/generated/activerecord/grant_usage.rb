class GrantUsage < ApplicationRecord
  self.table_name = "GrantUsage"
  self.primary_key = "grantId"

  attribute :viewCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :projectId, presence: true
end
