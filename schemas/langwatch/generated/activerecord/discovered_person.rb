class DiscoveredPerson < ApplicationRecord
  self.table_name = "DiscoveredPerson"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true, uniqueness: { scope: [ :provider, :rawActorId ] }
  validates :provider, presence: true
  validates :rawActorId, presence: true
  validates :displayText, presence: true
  validates :kind, presence: true
  validates :firstSeenAt, presence: true
  validates :lastSeenAt, presence: true
end
