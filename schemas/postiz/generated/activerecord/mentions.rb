class Mentions < ApplicationRecord
  self.table_name = "Mentions"
  self.primary_key = %w[name username platform image]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt
end
