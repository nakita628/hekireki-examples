class Jwks < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :publicKey, presence: true
  validates :privateKey, presence: true
end
