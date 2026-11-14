class VerificationToken < ApplicationRecord
  self.table_name = "VerificationToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true, uniqueness: { scope: :token }
  validates :token, presence: true
  validates :expires, presence: true
end
