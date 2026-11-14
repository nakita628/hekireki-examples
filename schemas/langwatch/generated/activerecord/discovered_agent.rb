class DiscoveredAgent < ApplicationRecord
  self.table_name = "DiscoveredAgent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true, uniqueness: { scope: [ :provider, :rawAgentId ] }
  validates :provider, presence: true
  validates :rawAgentId, presence: true
  validates :displayText, presence: true
  validates :metadata, exclusion: { in: [ nil ] }
  validates :firstSeenAt, presence: true
  validates :lastSeenAt, presence: true
end
