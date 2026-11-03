class MastraKnowledgeSemanticOutbox < ApplicationRecord
  self.table_name = "mastra_knowledge_semantic_outbox"

  alias_attribute :created_at, :createdAt

  validates :idempotencyKey, presence: true, uniqueness: true
  validates :documentId, presence: true
  validates :documentType, presence: true
  validates :operation, presence: true
  validates :scope, exclusion: { in: [ nil ] }
  validates :scopeKey, presence: true
  validates :status, presence: true
  validates :attempts, presence: true
  validates :availableAt, presence: true
end
