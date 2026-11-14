class JoinRequest < ApplicationRecord
  self.table_name = "JoinRequest"

  attribute :origin, default: "web"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, presence: true
  validates :organizationId, presence: true
  validates :domain, presence: true
  validates :state, presence: true
  validates :matchedVia, presence: true
  validates :occurredAt, presence: true
  validates :lastEventId, presence: true
  validates :acceptedAt, presence: true
  validates :projectionVersion, presence: true
end
