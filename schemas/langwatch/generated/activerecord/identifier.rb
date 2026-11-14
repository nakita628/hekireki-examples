class Identifier < ApplicationRecord
  self.table_name = "Identifier"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, presence: true
  validates :provider, presence: true
  validates :state, presence: true
  validates :attachedAt, presence: true
end
