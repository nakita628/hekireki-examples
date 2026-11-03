class MastraKnowledgeCursors < ApplicationRecord
  self.primary_key = %w[sourceThreadId agent]

  alias_attribute :updated_at, :updatedAt

  validates :lastKnowledgeId, presence: true
end
