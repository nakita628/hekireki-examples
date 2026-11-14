class PlatformToolPolicy < ApplicationRecord
  self.table_name = "PlatformToolPolicy"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :allowVk, default: true
  attribute :allowOtelDirect, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true, uniqueness: { scope: :toolSlug }
  validates :toolSlug, presence: true
end
