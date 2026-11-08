class License < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :maxUsers, default: 1
  alias_attribute :created_at, :createdAt

  validates :key, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :licenses
end
