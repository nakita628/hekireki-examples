class AnonymousVerificationToken < ApplicationRecord
  self.table_name = "AnonymousVerificationToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :expiresAt, presence: true
end
