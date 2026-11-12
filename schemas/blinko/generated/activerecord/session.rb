class Session < ApplicationRecord
  self.table_name = "session"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :sid, presence: true, uniqueness: true
  validates :data, presence: true
  validates :expiresAt, presence: true
end
