class IngestionSource < ApplicationRecord
  self.table_name = "IngestionSource"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :parserConfig, default: -> { JSON.parse("{}") }
  attribute :errorCount, default: 0
  attribute :status, default: "awaiting_first_event"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :sourceType, presence: true
  validates :name, presence: true, uniqueness: { scope: :organizationId }
  validates :ingestSecretHash, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :ingestion_sources
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :ingestion_sources
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :ingestion_sources_created
end
