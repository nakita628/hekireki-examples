class ConnectedCreditGrant < ApplicationRecord
  self.table_name = "ConnectedCreditGrant"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :kind, { commit: "commit", added: "added", renewal: "renewal" }, validate: true

  validates :accountId, presence: true
  validates :stripeCreditGrantId, presence: true, uniqueness: true
  validates :amountUsdCents, presence: true
  validates :termEndsAt, presence: true
  validates :expiresAt, presence: true
end
