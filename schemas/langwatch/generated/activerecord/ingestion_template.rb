class IngestionTemplate < ApplicationRecord
  self.table_name = "IngestionTemplate"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :ottlRules, default: ""
  attribute :platformPublished, default: false
  attribute :enabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :slug, presence: true,
                   uniqueness: { scope: :organizationId, conditions: -> { where.not(organizationId: nil) } }
  validates :sourceType, presence: true
  validates :displayName, presence: true

  belongs_to :organization, foreign_key: "organizationId", optional: true, inverse_of: :ingestion_templates
end
