class Salt < ApplicationRecord
  self.primary_key = "salt"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt
end
