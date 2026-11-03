class IntegrationAttributeSync < ApplicationRecord
  self.table_name = "IntegrationAttributeSync"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :integration, presence: true
  validates :enabled, inclusion: { in: [ true, false ] }

  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", inverse_of: :integration_attribute_syncs
  belongs_to :credential, foreign_key: "credentialId", optional: true, inverse_of: :integration_attribute_syncs
  has_one :attribute_sync_rule, foreign_key: "integrationAttributeSyncId", inverse_of: :integration_attribute_sync,
                                dependent: :destroy
  has_many :sync_field_mappings, class_name: "AttributeSyncFieldMapping", foreign_key: "integrationAttributeSyncId",
                                 inverse_of: :integration_attribute_sync, dependent: :destroy
end
