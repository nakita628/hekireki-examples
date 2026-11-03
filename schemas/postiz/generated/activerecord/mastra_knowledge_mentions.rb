class MastraKnowledgeMentions < ApplicationRecord
  self.primary_key = %w[sourceType sourceId recordId]
end
