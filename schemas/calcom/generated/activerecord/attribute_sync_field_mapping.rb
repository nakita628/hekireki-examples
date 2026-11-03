class AttributeSyncFieldMapping < ApplicationRecord
  self.table_name = "AttributeSyncFieldMapping"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :integrationFieldName, presence: true
  validates :enabled, inclusion: { in: [ true, false ] }
  validates :integrationAttributeSyncId, uniqueness: { scope: :attributeId }

  belongs_to :attribute_sync_field_mapping_attribute, class_name: "Attribute", foreign_key: "attributeId",
                                                      inverse_of: :attribute_sync_field_mappings
  belongs_to :integration_attribute_sync, foreign_key: "integrationAttributeSyncId", inverse_of: :sync_field_mappings
end
