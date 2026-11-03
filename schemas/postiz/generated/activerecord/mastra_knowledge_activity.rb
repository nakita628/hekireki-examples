class MastraKnowledgeActivity < ApplicationRecord
  self.table_name = "mastra_knowledge_activity"

  alias_attribute :created_at, :createdAt

  validates :action, presence: true
  validates :recordType, presence: true
  validates :recordId, presence: true
  validates :scope, exclusion: { in: [ nil ] }
  validates :scopeKey, presence: true
end
