class ConnectorPackage < ApplicationRecord
  self.table_name = "ConnectorPackage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :packageType, default: "airbyte"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :packageId, presence: true
end
