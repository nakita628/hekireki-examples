class Role < ApplicationRecord
  self.table_name = "Role"

  attribute :kind, default: "custom"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true
  validates :name, presence: true
  validates :permissions, exclusion: { in: [ nil ] }
  validates :occurredAt, presence: true
end
