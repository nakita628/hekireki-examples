class ModelProvider < ApplicationRecord
  self.table_name = "ModelProvider"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :rotationPolicy, { manual: "MANUAL" }, default: :manual, validate: true
  enum :healthStatus, { unknown: "UNKNOWN", healthy: "HEALTHY", degraded: "DEGRADED", circuit_open: "CIRCUIT_OPEN" },
       default: :unknown, validate: true

  validates :name, presence: true
  validates :provider, presence: true
  validates :enabled, inclusion: { in: [ true, false ] }
  validates :organizationId, presence: true,
                             uniqueness: { scope: :routingHandle, conditions: -> { where.not(routingHandle: nil) } }

  has_many :scopes, class_name: "ModelProviderScope", foreign_key: "modelProviderId", inverse_of: :model_provider,
                    dependent: :destroy
end
