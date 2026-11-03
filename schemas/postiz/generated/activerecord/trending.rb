class Trending < ApplicationRecord
  self.table_name = "Trending"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :trendingList, presence: true
  validates :language, uniqueness: { allow_nil: true }
  validates :hash, presence: true
  validates :date, presence: true
end
