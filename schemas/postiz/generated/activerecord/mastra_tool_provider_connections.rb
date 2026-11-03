class MastraToolProviderConnections < ApplicationRecord
  self.primary_key = %w[authorId providerId connectionId]

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :toolkit, presence: true
  validates :scope, presence: true
end
