class IdentityMatch < ApplicationRecord
  self.table_name = "IdentityMatch"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  validates :organizationId, presence: true
  validates :discoveredPersonId, presence: true
  validates :evidenceKind, presence: true
  validates :validFrom, presence: true
end
