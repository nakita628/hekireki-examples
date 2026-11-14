class RoutingPolicy < ApplicationRecord
  self.table_name = "RoutingPolicy"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :modelProviderIds, default: -> { JSON.parse("[]") }
  attribute :modelAliases, default: -> { JSON.parse("{}") }
  attribute :policyRules, default: -> { JSON.parse("{}") }
  attribute :isDefault, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :routing_policies
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :routing_policies_created
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", optional: true,
                          inverse_of: :routing_policies_updated
  has_many :virtual_keys, foreign_key: "routingPolicyId", inverse_of: :routing_policy, dependent: :nullify
  has_many :scopes, class_name: "RoutingPolicyScope", foreign_key: "routingPolicyId", inverse_of: :routing_policy,
                    dependent: :destroy
end
