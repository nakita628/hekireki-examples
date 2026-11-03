class MastraFavorites < ApplicationRecord
  self.primary_key = %w[userId entityType entityId]

  alias_attribute :created_at, :createdAt
end
