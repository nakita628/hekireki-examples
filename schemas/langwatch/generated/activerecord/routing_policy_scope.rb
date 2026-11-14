class RoutingPolicyScope < ApplicationRecord
  self.table_name = "RoutingPolicyScope"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT" }, validate: true

  validates :scopeId, presence: true, uniqueness: { scope: [ :routingPolicyId, :scopeType ] }

  belongs_to :routing_policy, foreign_key: "routingPolicyId", inverse_of: :scopes
end
