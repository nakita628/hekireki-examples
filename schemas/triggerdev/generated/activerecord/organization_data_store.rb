class OrganizationDataStore < ApplicationRecord
  self.table_name = "OrganizationDataStore"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :organizationIds, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :kind, { clickhouse: "CLICKHOUSE" }, validate: true

  validates :key, presence: true, uniqueness: true
  validates :config, exclusion: { in: [ nil ] }
end
