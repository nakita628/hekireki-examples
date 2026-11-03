class PopularPosts < ApplicationRecord
  self.table_name = "PopularPosts"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :category, presence: true
  validates :topic, presence: true
  validates :content, presence: true
  validates :hook, presence: true
end
