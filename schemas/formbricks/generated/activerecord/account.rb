class Account < ApplicationRecord
  self.table_name = "Account"
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :provider, presence: true, uniqueness: { scope: :providerAccountId }
  validates :providerAccountId, presence: true
  validates :issuer, uniqueness: { scope: :providerAccountId, allow_nil: true }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :accounts
end
