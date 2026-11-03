class AttributeSyncRule < ApplicationRecord
  self.table_name = "AttributeSyncRule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :integrationAttributeSyncId, uniqueness: true
  validates :rule, exclusion: { in: [ nil ] }

  belongs_to :integration_attribute_sync, foreign_key: "integrationAttributeSyncId", inverse_of: :attribute_sync_rule
end
