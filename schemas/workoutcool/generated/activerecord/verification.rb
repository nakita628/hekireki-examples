class Verification < ApplicationRecord
  self.table_name = "verification"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true
  validates :value, presence: true
  validates :expiresAt, presence: true
end
